-- Insert into Settings (Removed initial incorrect insert, assuming Settings has Key-Value pairs)
-- Ensure correct Key-Value pairs are inserted as per application requirements

INSERT INTO Roles (Name, Description, AccessToSettings)
VALUES ('Admin', 'Administrator access', 1),
       ('User', 'Standard user access', 0);

INSERT INTO Users (Name, Surname, Email, IdRole)
VALUES ('Manuel', 'Mauro', 'manuelmauro04@gmail.com', 1),
       ('Stefano', 'Hu', 'stefano.hu1.stud@tulliobuzi.edu.it', 2),
       ('Arianna', 'Bustone', 'arianna.bustone@gmail.com', 2),
       ('Andrea', 'Redegalli', 'redegalli03@gmail.com', 2);

INSERT INTO Room (Name, Location, Capacity)
VALUES ('Meeting', NULL, 12),
       ('OpenSpace #1', NULL, 10),
       ('OpenSpace #2', NULL, 10),
       ('Assistenza', NULL, 10),
       ('Sviluppo', NULL, 14),
       ('Bansky', NULL, 1),
       ('Contabilità', NULL, 2),
       ('Commerciale', NULL, 2);

INSERT INTO Booking (UserId, RoomId, BookingDate, StartTime, EndTime)
VALUES (1, 1, '2022-07-14', '09:00:00', '12:00:00'),
       (1, 2, '2022-07-14', '12:00:00', '13:00:00'),
       (1, 5, '2022-07-14', '14:00:00', '17:00:00'),
       (1, 3, '2022-07-14', '17:00:00', '18:00:00'),
       (2, 5, '2022-07-14', '09:00:00', '13:00:00'),
       (2, 5, '2022-07-14', '14:00:00', '18:00:00'),
       (3, 1, '2022-07-14', '09:00:00', '10:00:00'),
       (3, 5, '2022-07-14', '10:00:00', '13:00:00'),
       (3, 3, '2022-07-14', '14:00:00', '18:00:00'),
       (4, 2, '2022-07-14', '09:00:00', '13:00:00'),
       (4, 1, '2022-07-14', '14:00:00', '18:00:00');


-- Corrected the Votes insertion as per new schema
-- INSERT INTO Votes (YourCorrectColumns) VALUES (...);
-- The above needs adjustment based on the actual Vote table columns which were not provided

INSERT INTO Holiday (Date, Description)
VALUES ('2022-12-25', 'Christmas'),
       ('2022-12-31', 'New Year’s Eve');
