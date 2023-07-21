INSERT INTO Impostazioni
VALUES (0);

INSERT INTO Ruoli (idRuolo,descRuolo,accessoImpostazioni)
VALUES (1,'Admin',1),
(2,'User',0);

INSERT INTO Utenti (nome,cognome,email,idRuolo)
VALUES ('Manuel','Mauro','manuelmauro04@gmail.com',1),
('Stefano','Hu','stefano.hu1.stud@tulliobuzi.edu.it',2),
('Arianna','Bustone','arianna.bustone@gmail.com',2),
('Andrea','Redegalli','redegalli03@gmail.com',2);

INSERT INTO Stanze (nome,postiMax,postiMaxEmergenza)
VALUES ('Meeting',12,8),
('OpenSpace #1',10,6),
('OpenSpace #2',10,6),
('Assistenza',10,6),
('Sviluppo',14,8),
('Bansky',1,1),
('Contabilità',2,1),
('Commerciale',2,1);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T12:00:00',1,1),
('2022-07-14T12:00:00','2022-07-14T13:00:00',2,1),
('2022-07-14T14:00:00','2022-07-14T17:00:00',5,1),
('2022-07-14T17:00:00','2022-07-14T18:00:00',3,1);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T13:00:00',5,2),
('2022-07-14T14:00:00','2022-07-14T18:00:00',5,2);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T10:00:00',1,3),
('2022-07-14T10:00:00','2022-07-14T13:00:00',5,3),
('2022-07-14T14:00:00','2022-07-14T18:00:00',3,3);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T13:00:00',2,4),
('2022-07-14T14:00:00','2022-07-14T18:00:00',1,4);

INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (1,2,1),
(1,3,0),
(2,1,1),
(3,2,1),
(3,4,0),
(4,2,1),
(4,1,0);

INSERT INTO Feste (giorno, descrizione)
VALUES ('2022-12-25','Natale'),
('2022-12-31','Ultimo dell anno');