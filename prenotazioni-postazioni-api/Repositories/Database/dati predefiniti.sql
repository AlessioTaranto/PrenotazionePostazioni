-- Insert into Settings (Key, Value) -- Assuming you have specific keys and values to insert
-- INSERT INTO Settings ([Key], [Value]) VALUES ('YourKey', 'YourValue'), ...;

INSERT INTO Roles ([Name], [Description], AccessToSettings)
VALUES ('Admin', 'Administrator access', 1),
       ('User', 'Standard user access', 0);

INSERT INTO Room ([Name], [Location], Capacity)
VALUES ('Meeting', NULL, 12),
       ('OpenSpace #1', NULL, 10),
       ('OpenSpace #2', NULL, 10),
       ('Assistenza', NULL, 10),
       ('Sviluppo', NULL, 14),
       ('Bansky', NULL, 1),
       ('Contabilità', NULL, 2),
       ('Commerciale', NULL, 2);

-- Insert into Holiday ([Date], [Description])
INSERT INTO Holiday ([Date], [Description])
VALUES ('2022-12-25', 'Christmas'),
       ('2022-12-31', 'New Year’s Eve');

INSERT INTO Settings([modEmergency])
VALUES (0);