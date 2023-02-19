let presenti = 10;
let festivita = [];

function loadPresenti() {
    document.getElementById('lista-presenti').innerHTML = "";

    for(let i=0; i<presenti; i++) {
        let newLine = document.createElement('li');
        newLine.setAttribute("style", "width:40vw");
        newLine.innerHTML+='<img src="img/example/example-1.png" alt="example">Persona '+i;
        document.getElementById('lista-presenti').appendChild(newLine);
    }
}

function Festa(id, date, description) {
    return { id: id, date: date, description: description };
}

function addFesta(festa) {
    festivita.push(festa);
}

function addFesta(id, date, description) {
    festivita.push(Festa(id, date, description));
}

function removeFesta(festa) {
    festivita.pop(festa);
}

function removeFesta(id, date, description) {
    festivita.pop(Festa(id, date, description));
}

function isFesta(date) {
    for (var i in festivita)
        if (festivita[i].date === date)
            return true;
    return false;
}