let presenti = 10;
var festivita = [];
var nFestivita = 0;

function loadPresenti() {
    document.getElementById('lista-presenti').innerHTML = "";

    for(let i=0; i<presenti; i++) {
        let newLine = document.createElement('li');
        newLine.setAttribute("style", "width:40vw");
        newLine.innerHTML+='<img src="img/example/example-1.png" alt="example">Persona '+i;
        document.getElementById('lista-presenti').appendChild(newLine);
    }
}

function impostaFesta(date) {
    let day = date.getDate();
    let month = date.getMonth();
    let year = date.getFullYear();
    festivita.push({
        day:day,
        month:month,
        year:year
    });
    nFestivita++;
    let festa = document.createElement('li');
    festa.setAttribute("id", date);
    festa.setAttribute("style", "list-style-type:none; margin: 0px 0px 10px 0px");
    festa.innerText = day + "/" + month + "/" + year;
    document.getElementById("lista-feste").appendChild(festa);
}

function rimuoviFesta(date) {
    let day = date.getDate();
    let month = date.getMonth();
    let year = date.getFullYear();
    for(let i=0; i<nFestivita; i++) {
        if(festivita[i].day === day && festivita[i].month === month && festivita[i].year === year) {
            festivita.splice(i, 1);
            nFestivita--;
            document.getElementById(date).remove();
            return;
        }
    }
}