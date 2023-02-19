// Data utilizzata dal calendario per caricare i giorni (è un mese avanti)
let date = new Date;
// Mese corrente del calendario 
let month = date.getMonth();
// Giorno selezionato
let daySelected = new Date();
// Id del giorno selezionato, utilizzato per rendere nella visualizzazione il background arancione
let dayIdSelected = null;


// Ricarica il calendario con i dati correnti
function loadCalendar() {
    date.setDate(1);

    $('#month-year').text(translateMonth(date.getMonth()) + " " + date.getFullYear());

    month = date.getMonth();

    for (let i = 0; i<7; i++)
        for (let j = 0; j<6; j++) {
            $('#'.concat(i).concat('-').concat(j)).text("");
            $('#'.concat(i).concat('-').concat(j)).css("color", "black");
        }

    // Imposta il primo giorno del mese alla casella corretta e sposta l'index tot posizioni indietro
    let firstLineLoad = true;

    for (let i = 0; i<6; i++)
        for (let j = 0; j < 7; j++) {
            if (firstLineLoad === true) {
                date.setDate(date.getDate()-((date.getDay() === 0 ? +6 : date.getDay()-1)));
                firstLineLoad = false;
            }
            $('#'.concat(j).concat('-').concat(i)).text(date.getDate());
            if (date.getMonth() !== month)
                $('#'.concat(j).concat('-').concat(i)).css("color", "#a6a6a6");
            else if (isFesta(date))
                $('#'.concat(j).concat('-').concat(i)).css("color", "darkorange");
            
            date.setDate(date.getDate() + 1);
        }

    checkSelected();
}

// Naviga al mese precedente
function prevMonth() {
    // Il mese della data è sempre un mese avanti, quindi vengono tolti due mesi
    date.setMonth(date.getMonth() - 2);
    loadCalendar();
}

// naviga al mese successivo
function nextMonth() {
    // Siccome il mese salvato è un mese avanti, basta aggiornare il calendario
    loadCalendar();
}

// Questa funzione serve per convertire il numero restituito dalla data javascript in mese string
function translateMonth(number) {
    switch (number) {
        case 0:
            return "Gennaio";
        case 1:
            return "Febbraio";
        case 2:
            return "Marzo";
        case 3:
            return "Aprile";
        case 4:
            return "Maggio";
        case 5:
            return "Giugno";
        case 6:
            return "Luglio";
        case 7:
            return "Agosto";
        case 8:
            return "Settembre";
        case 9:
            return "Ottobre";
        case 10:
            return "Novembre";
        case 11:
            return "Dicembre";
    }
    return "";
}

// Controlla se nel calendario, al mese selezionato, è presente un giorno selezionato e lo evidenzia
function checkSelected() {
    if (daySelected.getMonth() !== date.getMonth() || daySelected.getFullYear() !== date.getFullYear())
        deselectCell(dayIdSelected);
    else
        selectCell(dayIdSelected);
}

// Sposta il giorno selezionato
function selectDay(date) {
    daySelected = date;
}

// Ottiene l'id della casella di un giorno, passato come parametro. Null se il giorno non è presente nel calendario
function getIdDay(number) {
    for (let i = 0; i < 6; i++)
        for (let j = 0; j < 7; j++)
            if ($('#'.concat(j).concat('-').concat(i)).css("color") === "rgb(0, 0, 0)" &&
                $('#'.concat(j).concat('-').concat(i)).text() == number)
                return j + '-' + i;
    return null;
}

// Clicca il calendario ad una casella passata come paramentro e cambia il giorno selezionato
function clickCalendar(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") === "rgb(0, 0, 0)") {
        checkSelectedCell(dayIdSelected);
        dayIdSelected = id;
        selectCell(id);
        selectDay(new Date(date.getFullYear(), date.getMonth(), selector.text()));
    }
}

// Se la casella ha l'attributo css "color" === "black", allora è un giorno appartente al mese corrente
function isValidDay(id) {
    return $('#'.concat(id)).css("color") === "rgb(0, 0, 0)";
}

// Sposta il calendario a una data selezionata
function goDate(newDate) {
    date = newDate;
    loadCalendar();
}

// Controlla se una casella è selezionata, utilizzato solo con dayIdSelected
function checkSelectedCell(id) {
    if (id !== null) {
        $('#'.concat(id)).css("color", "black");
        $('#'.concat(id)).css("background-color", "transparent");
        $('#'.concat(id)).css("font-weight", "normal");
    }
}

// Seleziona la casella, utilizzato solo con dayIdSelected
function selectCell(id) {
    if (id !== null) {
        $('#'.concat(id)).css("font-weight", "bold");
        $('#'.concat(id)).css("color", "white");
        $('#'.concat(id)).css("background-color", "darkorange");
    }
}

// Deseleziona o non selezionare la casella, utilizzato solo con dayIdSelected
function deselectCell(id) {
    if (id !== null) {
        $('#'.concat(id)).css("color", "black");
        $('#'.concat(id)).css("background-color", "transparent");
        $('#'.concat(id)).css("font-weight", "normal");
    }
}