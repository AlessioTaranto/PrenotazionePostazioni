let rumContabilita = 10;
let rumBansky = 20;
let rumOpen1 = 30;
let rumOpen2 = 40;
let rumMeeting = 50;
let rumCommerciale = 60;
let rumAssistenza = 70;
let rumSviluppo = 80;

function prenota() {
    let rum;
    let room = document.getElementById('room-sel').innerText;
    
    switch(room) {
        case 'Contabilità':
            rum = rumContabilita;
            break;
        case 'Bansky':
            rum = rumBansky;
            break;
        case 'OpenSpace #1':
            rum = rumOpen1;
            break;
        case 'OpenSpace #2':
            rum = rumOpen2;
            break;
        case 'Meeting':
            rum = rumMeeting;
            break;
        case 'Commerciale':
            rum = rumCommerciale;
            break;
        case 'Assistenza':
            rum = rumAssistenza;
            break;
        case 'Sviluppo':
            rum = rumSviluppo;
            break;
        default:
            return;
    }
}