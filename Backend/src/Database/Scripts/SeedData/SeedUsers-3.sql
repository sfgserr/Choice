INSERT INTO users."Users" ("Id", "Name", "Email", "PhoneNumber", "IconUri", "IsDataFilled", "Role", "Street", "City", "Latitude", "Longitude", "ReviewsCount", "AverageGrade") VALUES ('954dba19-f9bd-488a-9f45-dee4f184e906', 'Автодом', 'a@gmail.com', '9267339970', 'default.png', true, 'Company', 'Ангарская 21', 'Москва', '55.876824', '37.516799', 0, 0);

INSERT INTO users."Companies" ("Id", "UserId", "Description", "IsPrepaymentAvailable", "PhotoUris", "CategoriesId") VALUES ('954dba19-f9bd-488a-9f45-dee4f184e906', '954dba19-f9bd-488a-9f45-dee4f184e906', 'Ремонт автомобилей, продажа автозапчастей', true, '{"first.jpg", "snd.jpg", "trd.jpg"}', '{1}');

INSERT INTO users."Users" ("Id", "Name", "Email", "PhoneNumber", "IconUri", "IsDataFilled", "Role", "Street", "City", "Latitude", "Longitude", "ReviewsCount", "AverageGrade") VALUES ('ecc1d96a-dfd3-4a5c-8885-6914ff716ce2', 'Макар Чебан', 'b@gmail.com', '9267339971', 'default.png', true, 'Client', 'Арбат 26', 'Москва', '55.749896', '37.591530', 0, 0);

INSERT INTO users."Clients" ("Id", "UserId") VALUES ('ecc1d96a-dfd3-4a5c-8885-6914ff716ce2', 'ecc1d96a-dfd3-4a5c-8885-6914ff716ce2');

-- INSERT INTO payments."Subscriptions" ("Id", "SubscriberId", "PeriodName", "PeriodCost", "PeriodCostCurrency", "Status", "ExpirationDate") VALUES ('bb5d871c-e812-4a8a-ac2c-e3a2cafea641', '954dba19-f9bd-488a-9f45-dee4f184e906', 'Month', '800', 'RUB', 'Active', '2025-04-05');

INSERT INTO identity."Users" ("Id", "Email", "PhoneNumber", "HashedPassword", "Role", "Street", "City", "Latitude", "Longitude", "IsSubscribed") VALUES ('ecc1d96a-dfd3-4a5c-8885-6914ff716ce2', 'b@gmail.com', '9267339971', 'AE8ZXJlUbO+mw/xjWJ/UQxTyCSuDs6WjTUJpMHs03a2ewBoe1AG7hCrYvZW+j+1Tng==', 'Client', 'Арбат 26', 'Москва', '55.749896', '37.591530', true);

INSERT INTO identity."Users" ("Id", "Email", "PhoneNumber", "HashedPassword", "Role", "Street", "City", "Latitude", "Longitude", "IsSubscribed") VALUES ('954dba19-f9bd-488a-9f45-dee4f184e906', 'a@gmail.com', '9267339970', 'AE8ZXJlUbO+mw/xjWJ/UQxTyCSuDs6WjTUJpMHs03a2ewBoe1AG7hCrYvZW+j+1Tng==', 'Company', 'Ангарская 21', 'Москва', '55.876824', '37.516799', true);

INSERT INTO identity."Users" ("Id", "Email", "PhoneNumber", "HashedPassword", "Role", "Street", "City", "Latitude", "Longitude", "IsSubscribed") VALUES ('92a69085-320f-4dbd-9dcc-bb48ffbe40ae', 'aleshkin@gmail.com', '1111111111', 'AE8ZXJlUbO+mw/xjWJ/UQxTyCSuDs6WjTUJpMHs03a2ewBoe1AG7hCrYvZW+j+1Tng==', 'Admin', 'Омск', 'Омск', '0', '0', true);

INSERT INTO chat."ChatUsers" ("Id", "Name", "IconUri") VALUES ('954dba19-f9bd-488a-9f45-dee4f184e906', 'Автодом', 'default.png');

INSERT INTO chat."ChatUsers" ("Id", "Name", "IconUri") VALUES ('ecc1d96a-dfd3-4a5c-8885-6914ff716ce2', 'Макар Чебан', 'default.png');

INSERT INTO payments."Wallets" ("Id", "PayerId", "Copecks") VALUES ('84b92f6d-a254-4b4d-a115-d5a38e44e58d', 'ecc1d96a-dfd3-4a5c-8885-6914ff716ce2', 15000);

INSERT INTO payments."Wallets" ("Id", "PayerId", "Copecks") VALUES ('3fd3df18-9e32-44a2-9f2e-f79d0d377249', '954dba19-f9bd-488a-9f45-dee4f184e906', 0);
