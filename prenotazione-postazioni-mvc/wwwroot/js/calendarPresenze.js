let date1 = new Date;
let month1 = date1.getMonth();
let daySelected1 = new Date();
let dayIdSelected1 = null;

function loadCalendar1() {
    date1.setDate(1);

    $('#month-year-1').text(translateMonth(date1.getMonth()) + " " + date1.getFullYear());
    month1 = date1.getMonth();

    for (let i = 0; i < 7; i++)
        for (let j = 0; j < 6; j++) {
            $('#'.concat(i).concat('-').concat(j).concat('1')).text("");
            $('#'.concat(i).concat('-').concat(j).concat('1')).css("color", "black");
        }

    let firstLineLoad = true;

    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 7; j++) {
            if (firstLineLoad === true) {
                date1.setDate(date1.getDate() - ((date1.getDay() === 0 ? +6 : date1.getDay() - 1)));
                firstLineLoad = false;
            }
            $('#'.concat(j).concat('-').concat(i).concat('1')).text(date1.getDate());
            if (date1.getMonth() !== month1)
                $('#'.concat(j).concat('-').concat(i).concat('1')).css("color", "#a6a6a6");
            else if (getFesta(date))
                $('#'.concat(getFesta(date).id.replace('f', ''))).css("color", "darkorange");

            date1.setDate(date1.getDate() + 1);
        }
    }

    checkSelected1();
}

function nextMonth1() {
    date1.setMonth(date1.getMonth());
    loadCalendar1();
}

function prevMonth1() {
    date1.setMonth(date1.getMonth() - 2);
    loadCalendar1();
}

function checkSelected1() {
    if (daySelected1.getMonth() !== date1.getMonth() || daySelected1.getFullYear() !== date1.getFullYear()) {
        $('#'.concat(dayIdSelected1)).css("color", "black");
        $('#'.concat(dayIdSelected1)).css("background-color", "transparent");
        $('#'.concat(dayIdSelected1)).css("font-weight", "normal");
    } else {
        $('#'.concat(dayIdSelected1)).css("color", "white");
        $('#'.concat(dayIdSelected1)).css("background-color", "darkorange");
        $('#'.concat(dayIdSelected1)).css("font-weight", "bold");
    }
}

function selectDay1(date) {
    daySelected1 = date;
}

function getIdDay1(number) {
    for (let i = 0; i < 6; i++)
        for (let j = 0; j < 7; j++)
            if ($('#'.concat(j).concat('-').concat(i).concat(1)).css("color") === "rgb(0, 0, 0)" &&
                $('#'.concat(j).concat('-').concat(i).concat(1)).text() == number)
                return j + '-' + i + "" + 1;
    return null;
}

function goDate1(newDate) {
    date1 = newDate;
    loadCalendar1();
}

function clickCalendarPres(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") === "rgb(0, 0, 0)") {
        checkSelectedCell(dayIdSelected1);
        dayIdSelected1 = id;
        selectCell(id);
        selectDay1(new Date(date1.getFullYear(), date1.getMonth(), selector.text()));
    }
}