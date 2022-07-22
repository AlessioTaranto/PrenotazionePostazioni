// Ottieni una lista con tutte le date delle feste presenti in un mese e anno
function getFeste(month, year) {
    let festeInMonth = [];

    festivita.forEach(festa => {
        if (festa.date.getMonth() === month && festa.date.getFullYear() === year)
            festeInMonth.push(festa.date.getDate());
    });

    return festeInMonth;
}

// Ottieni l'oggetto della festività, quindi anche id associato. Null se la data non è una festa
function getFesta(dateFesta) {
    festivita.forEach(festa => {
        if (festa.date === dateFesta)
            return festa;
    });
    return null;
}

// Aggiorna il pulsante per impostare una festività
function festaButton() {
    if (dayIdSelected !== null) {
        let selector = $('#'.concat(dayIdSelected));
        let btn = $('#festabutton');

        if (btn.text() == "Imposta come festività") {
            btn.text("Rimuovi festività");
            selector.css("color", "darkorange");
            impostaFesta(daySelected, dayIdSelected);
        } else {
            btn.text("Imposta come festività");
            selector.css("color", "black");
            rimuoviFesta(daySelected, dayIdSelected);
        }

        loadCalendar();
        loadCalendar1();
        dayIdSelected = null;

        selector.css("background-color", "transparent");
        selector.css("font-weight", "normal");
    }
}

// Duplicato del clickCalendar in calendar.js, ma con controlli diversi, adatti al caendario delle Festività 
function clickCalendarFest(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") !== "rgb(255, 255, 255)") {
        checkSelectedCell(dayIdSelected);
        dayIdSelected = id;
        // In base al colore della casella cliccata (se è nero non è una festa), cambia il testo del pulsante #festabutton
        $('#festabutton').text(selector.css("color") === "rgb(0, 0, 0)" ? "Imposta come festività" : "Rimuovi festività");
        selectCell(id);
        selectDay(new Date(date.getFullYear(), date.getMonth(), selector.text()));
    }
}

function deserializeFeste(jsonFeste) {
    festivita = JSON.parse(jsonFeste);
}