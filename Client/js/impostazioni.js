let presenti = 10;

function loadPresenti() {
    document.getElementById('lista-presenti').innerHTML = "";

    for(let i=0; i<presenti; i++) {
        let newLine = document.createElement('li');
        newLine.setAttribute("style", "width:40vw");
        newLine.innerHTML+='<img src="img/example/example-1.png" alt="example">Persona'+i;
        document.getElementById('lista-presenti').appendChild(newLine);
    }
}