// noinspection JSJQueryEfficiency

let daySelected = new Date();
let dayIdSelected = null;

function selectDay(date) {
    daySelected = date;
}

//Link api google https://developers.google.com/people/api/rest/v1/people/get

function getIdDay(number) {
    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 7; j++) {
            if ($('#'.concat(j).concat('-').concat(i)).css("color") === "rgb(0, 0, 0)" &&
                $('#'.concat(j).concat('-').concat(i)).text() == number)
                return j + '-' + i;
        }
    }
    return "null";
}

function clickCalendar(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") !== "rgb(0, 0, 0)")
        return;

    if (dayIdSelected !== null) {
        $('#'.concat(dayIdSelected)).css("color","black");
        $('#'.concat(dayIdSelected)).css("background-color","transparent");
        $('#'.concat(dayIdSelected)).css("font-weight","normal");
    }

    dayIdSelected = id;

    selector.css("color","white");
    selector.css("background-color","darkorange");
    selector.css("font-weight","bold");

    selectDay(new Date(date.getFullYear(), date.getMonth(), selector.text()));
}

function isValidDay(id) {
    let selector = $('#'.concat(id));
    if (selector.css("color") !== "rgb(0, 0, 0)")
        return false;
    return true;
}

function goDate(newDate) {
    date = newDate;
    loadCalendar();
}