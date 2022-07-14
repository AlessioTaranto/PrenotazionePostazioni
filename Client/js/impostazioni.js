let presenti = 10;

function loadPresenti() {
    document.getElementById('lista-presenti').innerHTML = "";

    for(let i=0; i<presenti; i++) {
        let newLine = document.createElement('li');
        newLine.innerHTML+='<img src="img/example/example-1.png" alt="example"></li>';
        document.getElementById('lista-presenti').appendChild(newLine);
    }
}