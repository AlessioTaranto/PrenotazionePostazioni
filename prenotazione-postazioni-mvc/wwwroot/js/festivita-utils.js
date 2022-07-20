function getFeste(month, year) {
    let festeInMonth = [];

    festivita.forEach(festa => {
        if (festa.date.getMonth() === month && festa.date.getFullYear() === year)
            festeInMonth.push(festa.date.getDate());
    });

    return festeInMonth;
}

function getFesta(dateFesta) {
    festivita.forEach(festa => {
        if (festa.date === dateFesta)
            return festa;
    });
    return null;
}

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

function clickCalendarFest(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") !== "rgb(255, 255, 255)") {
        checkSelectedCell(dayIdSelected);
        dayIdSelected = id;
        $('#festabutton').text(selector.css("color") === "rgb(0, 0, 0)" ? "Imposta come festività" : "Rimuovi festività");
        selectCell(id);
        selectDay(new Date(date.getFullYear(), date.getMonth(), selector.text()));
    }

}