
const date = new Date;
let month = date.getMonth();

function loadCalendar() {
    date.setDate(1);

    $('#month-year').text(translateMonth(date.getMonth()) + " " + date.getFullYear());
    month = date.getMonth();

    for (let i = 0; i<7; i++)
        for (let j = 0; j<6; j++) {
            $('#' + i + '-' + j).text("");
            $('#'+i+'-'+j).css("color", "black");
        }

    let firstLineLoad = true

    for (let i = 0; i<6; i++) {
        for (let j = 0; j<7; j++) {
            if (firstLineLoad === true) {
                date.setDate(date.getDate()-((date.getDay() === 0 ? +6 : date.getDay()-1)));
                firstLineLoad = false;
            }
            $('#'+j+'-'+i).text(date.getDate());
            if (date.getMonth() !== month)
                $('#'+j+'-'+i).css("color", "#a6a6a6");
            date.setDate(date.getDate()+1);
        }
    }
}

function prevMonth() {
    date.setMonth(date.getMonth()-2);
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