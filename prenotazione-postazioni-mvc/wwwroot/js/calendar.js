let date = new Date;
let month = date.getMonth();
let daySelected = new Date();
let dayIdSelected = null;

function loadCalendar() {
    date.setDate(1);

    $('#month-year').text(translateMonth(date.getMonth()) + " " + date.getFullYear());

    month = date.getMonth();

    for (let i = 0; i<7; i++)
        for (let j = 0; j<6; j++) {
            $('#'.concat(i).concat('-').concat(j)).text("");
            $('#'.concat(i).concat('-').concat(j)).css("color", "black");
        }

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
            else if (getFesta(date))
                $('#'.concat(getFesta(date).id.replace('f', ''))).css("color", "darkorange");
            
            date.setDate(date.getDate() + 1);
        }

    checkSelected();
}

function prevMonth() {
    date.setMonth(date.getMonth() - 2);
    loadCalendar();
}

function nextMonth() {
    date.setMonth(date.getMonth());
    loadCalendar();
}

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

function daysInMonth (month) {
    return new Date(1970, month, 0).getDate();
}

function checkSelected() {
    if (daySelected.getMonth() !== date.getMonth() || daySelected.getFullYear() !== date.getFullYear())
        deselectCell(dayIdSelected);
    else
        selectCell(dayIdSelected);
}

function selectDay(date) {
    daySelected = date;
}

function getIdDay(number) {
    for (let i = 0; i < 6; i++)
        for (let j = 0; j < 7; j++)
            if ($('#'.concat(j).concat('-').concat(i)).css("color") === "rgb(0, 0, 0)" &&
                $('#'.concat(j).concat('-').concat(i)).text() == number)
                return j + '-' + i;
    return null;
}

function clickCalendar(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") === "rgb(0, 0, 0)") {
        checkSelectedCell(dayIdSelected);
        dayIdSelected = id;
        selectCell(id);
        selectDay(new Date(date.getFullYear(), date.getMonth(), selector.text()));
    }
}

function isValidDay(id) {
    return $('#'.concat(id)).css("color") === "rgb(0, 0, 0)";
}

function goDate(newDate) {
    date = newDate;
    loadCalendar();
}

function checkSelectedCell(id) {
    if (id !== null) {
        $('#'.concat(id)).css("color", "black");
        $('#'.concat(id)).css("background-color", "transparent");
        $('#'.concat(id)).css("font-weight", "normal");
    }
}

function selectCell(id) {
    if (id !== null) {
        $('#'.concat(id)).css("font-weight", "bold");
        $('#'.concat(id)).css("color", "white");
        $('#'.concat(id)).css("background-color", "darkorange");
    }
}

function deselectCell(id) {
    if (id !== null) {
        $('#'.concat(id)).css("color", "black");
        $('#'.concat(id)).css("background-color", "transparent");
        $('#'.concat(id)).css("font-weight", "normal");
    }
}