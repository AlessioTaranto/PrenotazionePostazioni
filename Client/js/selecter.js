// noinspection JSJQueryEfficiency

let roomSelected = null;
let daySelected = new Date();
let startHoursSelected = 9;
let startMinutesSelected = 0;
let finishHoursSelected = 13;
let finishMinutesSelected = 0;

let dayIdSelected = null;

function selectRoom(room) {
    roomSelected = room;
    $('#room-sel').text(roomSelected);
}

function selectDay(date) {
    daySelected = date;
    $('#day-sel').text("Giorno selezionato: " + date.getDate() + "/" + date.getMonth() + "/" +  date.getFullYear());
}

//Link api google https://developers.google.com/people/api/rest/v1/people/get

function defaultSelecter() {
    $('#room-sel').text("Seleziona una Stanza");
    selectDay(daySelected);
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