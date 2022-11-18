-- TA-144
DO
$$
    DECLARE
        cet_guid uuid := gen_random_uuid();
        cest_guid uuid := gen_random_uuid();
    BEGIN
        TRUNCATE general.time_zones CASCADE;
        TRUNCATE general.cities CASCADE;

        INSERT INTO general.time_zones(id, name, offset_from_utc, description)
        VALUES (cet_guid, 'CET', 1, '	Central European Time'),
               (cest_guid, 'CEST', 2, '	Central European Summer Time');

        INSERT INTO general.cities(id, name, time_zone_id, location, country)
        VALUES (gen_random_uuid(),'Warsaw',cet_guid,ST_GeomFromText('POINT (52.22 21.03)', 4326),'Poland'),
               (gen_random_uuid(),'Kraków',cet_guid,ST_GeomFromText('POINT (50.06 19.94)', 4326),'Poland'),
               (gen_random_uuid(),'Łódź',cet_guid,ST_GeomFromText('POINT (51.78 19.45)', 4326),'Poland'),
               (gen_random_uuid(),'Wrocław',cet_guid,ST_GeomFromText('POINT (51.11 17.03)', 4326),'Poland'),
               (gen_random_uuid(),'Poznań',cet_guid,ST_GeomFromText('POINT (52.4 16.92)', 4326),'Poland'),
               (gen_random_uuid(),'Gdańsk',cet_guid,ST_GeomFromText('POINT (54.37 18.63)', 4326),'Poland'),
               (gen_random_uuid(),'Szczecin',cet_guid,ST_GeomFromText('POINT (53.42 14.56)', 4326),'Poland'),
               (gen_random_uuid(),'Bydgoszcz',cet_guid,ST_GeomFromText('POINT (53.12 18)', 4326),'Poland'),
               (gen_random_uuid(),'Lublin',cet_guid,ST_GeomFromText('POINT (51.23 22.57)', 4326),'Poland'),
               (gen_random_uuid(),'Białystok',cet_guid,ST_GeomFromText('POINT (53.12 23.17)', 4326),'Poland'),
               (gen_random_uuid(),'Katowice',cet_guid,ST_GeomFromText('POINT (50.25 19)', 4326),'Poland'),
               (gen_random_uuid(),'Gdynia',cet_guid,ST_GeomFromText('POINT (54.52 18.53)', 4326),'Poland'),
               (gen_random_uuid(),'Częstochowa',cet_guid,ST_GeomFromText('POINT (50.81 19.12)', 4326),'Poland'),
               (gen_random_uuid(),'Radom',cet_guid,ST_GeomFromText('POINT (51.4 21.16)', 4326),'Poland'),
               (gen_random_uuid(),'Toruń',cet_guid,ST_GeomFromText('POINT (53.02 18.62)', 4326),'Poland'),
               (gen_random_uuid(),'Sosnowiec',cet_guid,ST_GeomFromText('POINT (50.3 19.17)', 4326),'Poland'),
               (gen_random_uuid(),'Kielce',cet_guid,ST_GeomFromText('POINT (50.87 20.63)', 4326),'Poland'),
               (gen_random_uuid(),'Rzeszów',cet_guid,ST_GeomFromText('POINT (50.05 22)', 4326),'Poland'),
               (gen_random_uuid(),'Gliwice',cet_guid,ST_GeomFromText('POINT (50.3 18.65)', 4326),'Poland'),
               (gen_random_uuid(),'Zabrze',cet_guid,ST_GeomFromText('POINT (50.3 18.78)', 4326),'Poland'),
               (gen_random_uuid(),'Olsztyn',cet_guid,ST_GeomFromText('POINT (53.78 20.49)', 4326),'Poland'),
               (gen_random_uuid(),'Bielsko-Biała',cet_guid,ST_GeomFromText('POINT (49.82 19.04)', 4326),'Poland'),
               (gen_random_uuid(),'Bytom',cet_guid,ST_GeomFromText('POINT (50.35 18.92)', 4326),'Poland'),
               (gen_random_uuid(),'Rybnik',cet_guid,ST_GeomFromText('POINT (50.08 18.5)', 4326),'Poland'),
               (gen_random_uuid(),'Ruda Śląska',cet_guid,ST_GeomFromText('POINT (50.26 18.85)', 4326),'Poland'),
               (gen_random_uuid(),'Zielona Góra',cet_guid,ST_GeomFromText('POINT (51.94 15.5)', 4326),'Poland'),
               (gen_random_uuid(),'Tychy',cet_guid,ST_GeomFromText('POINT (50.17 19)', 4326),'Poland'),
               (gen_random_uuid(),'Opole',cet_guid,ST_GeomFromText('POINT (50.67 17.93)', 4326),'Poland'),
               (gen_random_uuid(),'Gorzów Wielkopolski',cet_guid,ST_GeomFromText('POINT (52.73 15.25)', 4326),'Poland'),
               (gen_random_uuid(),'Dąbrowa Górnicza',cet_guid,ST_GeomFromText('POINT (50.32 19.24)', 4326),'Poland'),
               (gen_random_uuid(),'Płock',cet_guid,ST_GeomFromText('POINT (52.55 19.7)', 4326),'Poland'),
               (gen_random_uuid(),'Elbląg',cet_guid,ST_GeomFromText('POINT (54.17 19.4)', 4326),'Poland'),
               (gen_random_uuid(),'Wałbrzych',cet_guid,ST_GeomFromText('POINT (50.77 16.28)', 4326),'Poland'),
               (gen_random_uuid(),'Chorzów',cet_guid,ST_GeomFromText('POINT (50.3 18.95)', 4326),'Poland'),
               (gen_random_uuid(),'Tarnów',cet_guid,ST_GeomFromText('POINT (50.01 20.99)', 4326),'Poland'),
               (gen_random_uuid(),'Koszalin',cet_guid,ST_GeomFromText('POINT (54.19 16.18)', 4326),'Poland'),
               (gen_random_uuid(),'Kalisz',cet_guid,ST_GeomFromText('POINT (51.76 18.08)', 4326),'Poland'),
               (gen_random_uuid(),'Legnica',cet_guid,ST_GeomFromText('POINT (51.21 16.16)', 4326),'Poland'),
               (gen_random_uuid(),'Włocławek',cet_guid,ST_GeomFromText('POINT (52.65 19.05)', 4326),'Poland'),
               (gen_random_uuid(),'Grudziądz',cet_guid,ST_GeomFromText('POINT (53.49 18.78)', 4326),'Poland'),
               (gen_random_uuid(),'Jaworzno',cet_guid,ST_GeomFromText('POINT (50.2 19.27)', 4326),'Poland'),
               (gen_random_uuid(),'Jelenia Góra',cet_guid,ST_GeomFromText('POINT (50.9 15.73)', 4326),'Poland'),
               (gen_random_uuid(),'Siedlce',cet_guid,ST_GeomFromText('POINT (52.18 22.28)', 4326),'Poland'),
               (gen_random_uuid(),'Mysłowice',cet_guid,ST_GeomFromText('POINT (50.23 19.13)', 4326),'Poland'),
               (gen_random_uuid(),'Piotrków Trybunalski',cet_guid,ST_GeomFromText('POINT (51.4 19.68)', 4326),'Poland'),
               (gen_random_uuid(),'Konin',cet_guid,ST_GeomFromText('POINT (52.23 18.26)', 4326),'Poland'),
               (gen_random_uuid(),'Inowrocław',cet_guid,ST_GeomFromText('POINT (52.8 18.26)', 4326),'Poland'),
               (gen_random_uuid(),'Ostrowiec Świętokrzyski',cet_guid,ST_GeomFromText('POINT (50.93 21.4)', 4326),'Poland'),
               (gen_random_uuid(),'Suwałki',cet_guid,ST_GeomFromText('POINT (54.08 22.93)', 4326),'Poland'),
               (gen_random_uuid(),'Gniezno',cet_guid,ST_GeomFromText('POINT (52.53 17.6)', 4326),'Poland'),
               (gen_random_uuid(),'Przemyśl',cet_guid,ST_GeomFromText('POINT (49.78 22.78)', 4326),'Poland'),
               (gen_random_uuid(),'Głogów',cet_guid,ST_GeomFromText('POINT (51.66 16.08)', 4326),'Poland'),
               (gen_random_uuid(),'Zamość',cet_guid,ST_GeomFromText('POINT (50.72 23.26)', 4326),'Poland'),
               (gen_random_uuid(),'Pabianice',cet_guid,ST_GeomFromText('POINT (51.66 19.35)', 4326),'Poland'),
               (gen_random_uuid(),'Leszno',cet_guid,ST_GeomFromText('POINT (51.84 16.57)', 4326),'Poland'),
               (gen_random_uuid(),'Łomża',cet_guid,ST_GeomFromText('POINT (53.18 22.08)', 4326),'Poland'),
               (gen_random_uuid(),'Ełk',cet_guid,ST_GeomFromText('POINT (53.82 22.35)', 4326),'Poland'),
               (gen_random_uuid(),'Tarnowskie Góry',cet_guid,ST_GeomFromText('POINT (50.45 18.86)', 4326),'Poland'),
               (gen_random_uuid(),'Chełm',cet_guid,ST_GeomFromText('POINT (51.13 23.48)', 4326),'Poland'),
               (gen_random_uuid(),'Pruszków',cet_guid,ST_GeomFromText('POINT (52.17 20.8)', 4326),'Poland'),
               (gen_random_uuid(),'Mielec',cet_guid,ST_GeomFromText('POINT (50.28 21.43)', 4326),'Poland'),
               (gen_random_uuid(),'Bełchatów',cet_guid,ST_GeomFromText('POINT (51.37 19.37)', 4326),'Poland'),
               (gen_random_uuid(),'Świdnica',cet_guid,ST_GeomFromText('POINT (50.84 16.49)', 4326),'Poland'),
               (gen_random_uuid(),'Biała Podlaska',cet_guid,ST_GeomFromText('POINT (52.03 23.12)', 4326),'Poland'),
               (gen_random_uuid(),'Będzin',cet_guid,ST_GeomFromText('POINT (50.32 19.13)', 4326),'Poland'),
               (gen_random_uuid(),'Legionowo',cet_guid,ST_GeomFromText('POINT (52.4 20.93)', 4326),'Poland'),
               (gen_random_uuid(),'Ostrołęka',cet_guid,ST_GeomFromText('POINT (53.08 21.57)', 4326),'Poland'),
               (gen_random_uuid(),'Puławy',cet_guid,ST_GeomFromText('POINT (51.42 21.97)', 4326),'Poland'),
               (gen_random_uuid(),'Starogard Gdański',cet_guid,ST_GeomFromText('POINT (53.97 18.53)', 4326),'Poland'),
               (gen_random_uuid(),'Skierniewice',cet_guid,ST_GeomFromText('POINT (51.95 20.14)', 4326),'Poland'),
               (gen_random_uuid(),'Tarnobrzeg',cet_guid,ST_GeomFromText('POINT (50.58 21.68)', 4326),'Poland'),
               (gen_random_uuid(),'Starachowice',cet_guid,ST_GeomFromText('POINT (51.05 21.07)', 4326),'Poland'),
               (gen_random_uuid(),'Kutno',cet_guid,ST_GeomFromText('POINT (52.23 19.37)', 4326),'Poland'),
               (gen_random_uuid(),'Rumia',cet_guid,ST_GeomFromText('POINT (54.57 18.4)', 4326),'Poland'),
               (gen_random_uuid(),'Nysa',cet_guid,ST_GeomFromText('POINT (50.47 17.33)', 4326),'Poland'),
               (gen_random_uuid(),'Kołobrzeg',cet_guid,ST_GeomFromText('POINT (54.17 15.57)', 4326),'Poland'),
               (gen_random_uuid(),'Dębica',cet_guid,ST_GeomFromText('POINT (50.05 21.41)', 4326),'Poland'),
               (gen_random_uuid(),'Ciechanów',cet_guid,ST_GeomFromText('POINT (52.88 20.61)', 4326),'Poland'),
               (gen_random_uuid(),'Świdnik',cet_guid,ST_GeomFromText('POINT (51.23 22.7)', 4326),'Poland'),
               (gen_random_uuid(),'Otwock',cet_guid,ST_GeomFromText('POINT (52.12 21.27)', 4326),'Poland'),
               (gen_random_uuid(),'Sieradz',cet_guid,ST_GeomFromText('POINT (51.6 18.75)', 4326),'Poland'),
               (gen_random_uuid(),'Mikołów',cet_guid,ST_GeomFromText('POINT (50.17 18.9)', 4326),'Poland'),
               (gen_random_uuid(),'Świnoujście',cet_guid,ST_GeomFromText('POINT (53.92 14.25)', 4326),'Poland'),
               (gen_random_uuid(),'Kwidzyn',cet_guid,ST_GeomFromText('POINT (53.74 18.93)', 4326),'Poland'),
               (gen_random_uuid(),'Chojnice',cet_guid,ST_GeomFromText('POINT (53.7 17.56)', 4326),'Poland'),
               (gen_random_uuid(),'Nowa Sól',cet_guid,ST_GeomFromText('POINT (51.8 15.72)', 4326),'Poland'),
               (gen_random_uuid(),'Bolesławiec',cet_guid,ST_GeomFromText('POINT (51.27 15.57)', 4326),'Poland'),
               (gen_random_uuid(),'Oświęcim',cet_guid,ST_GeomFromText('POINT (50.03 19.23)', 4326),'Poland'),
               (gen_random_uuid(),'Malbork',cet_guid,ST_GeomFromText('POINT (54.03 19.04)', 4326),'Poland'),
               (gen_random_uuid(),'Żary',cet_guid,ST_GeomFromText('POINT (51.63 15.13)', 4326),'Poland'),
               (gen_random_uuid(),'Jarosław',cet_guid,ST_GeomFromText('POINT (50.02 22.68)', 4326),'Poland'),
               (gen_random_uuid(),'Sopot',cet_guid,ST_GeomFromText('POINT (54.44 18.55)', 4326),'Poland'),
               (gen_random_uuid(),'Chrzanów',cet_guid,ST_GeomFromText('POINT (50.13 19.4)', 4326),'Poland'),
               (gen_random_uuid(),'Brzeg',cet_guid,ST_GeomFromText('POINT (50.87 17.48)', 4326),'Poland'),
               (gen_random_uuid(),'Jasło',cet_guid,ST_GeomFromText('POINT (49.73 21.47)', 4326),'Poland'),
               (gen_random_uuid(),'Lębork',cet_guid,ST_GeomFromText('POINT (54.55 17.75)', 4326),'Poland'),
               (gen_random_uuid(),'Cieszyn',cet_guid,ST_GeomFromText('POINT (49.75 18.63)', 4326),'Poland'),
               (gen_random_uuid(),'Czeladź',cet_guid,ST_GeomFromText('POINT (50.33 19.08)', 4326),'Poland'),
               (gen_random_uuid(),'Kraśnik',cet_guid,ST_GeomFromText('POINT (50.92 22.22)', 4326),'Poland'),
               (gen_random_uuid(),'Nowy Targ',cet_guid,ST_GeomFromText('POINT (49.48 20.03)', 4326),'Poland'),
               (gen_random_uuid(),'Iława',cet_guid,ST_GeomFromText('POINT (53.6 19.57)', 4326),'Poland'),
               (gen_random_uuid(),'Police',cet_guid,ST_GeomFromText('POINT (53.55 14.57)', 4326),'Poland'),
               (gen_random_uuid(),'Dzierżoniów',cet_guid,ST_GeomFromText('POINT (50.73 16.65)', 4326),'Poland'),
               (gen_random_uuid(),'Grodzisk Mazowiecki',cet_guid,ST_GeomFromText('POINT (52.1 20.63)', 4326),'Poland'),
               (gen_random_uuid(),'Zgorzelec',cet_guid,ST_GeomFromText('POINT (51.15 15)', 4326),'Poland'),
               (gen_random_uuid(),'Łowicz',cet_guid,ST_GeomFromText('POINT (52.1 19.93)', 4326),'Poland'),
               (gen_random_uuid(),'Łuków',cet_guid,ST_GeomFromText('POINT (51.92 22.38)', 4326),'Poland'),
               (gen_random_uuid(),'Bielsk Podlaski',cet_guid,ST_GeomFromText('POINT (52.77 23.2)', 4326),'Poland'),
               (gen_random_uuid(),'Bielawa',cet_guid,ST_GeomFromText('POINT (50.69 16.62)', 4326),'Poland'),
               (gen_random_uuid(),'Augustów',cet_guid,ST_GeomFromText('POINT (53.85 22.97)', 4326),'Poland'),
               (gen_random_uuid(),'Bochnia',cet_guid,ST_GeomFromText('POINT (49.98 20.43)', 4326),'Poland'),
               (gen_random_uuid(),'Mława',cet_guid,ST_GeomFromText('POINT (53.12 20.37)', 4326),'Poland'),
               (gen_random_uuid(),'Giżycko',cet_guid,ST_GeomFromText('POINT (54.04 21.76)', 4326),'Poland'),
               (gen_random_uuid(),'Brodnica',cet_guid,ST_GeomFromText('POINT (53.25 19.4)', 4326),'Poland'),
               (gen_random_uuid(),'Krotoszyn',cet_guid,ST_GeomFromText('POINT (51.7 17.44)', 4326),'Poland'),
               (gen_random_uuid(),'Kętrzyn',cet_guid,ST_GeomFromText('POINT (54.08 21.38)', 4326),'Poland'),
               (gen_random_uuid(),'Zakopane',cet_guid,ST_GeomFromText('POINT (49.3 19.95)', 4326),'Poland'),
               (gen_random_uuid(),'Gorlice',cet_guid,ST_GeomFromText('POINT (49.66 21.16)', 4326),'Poland'),
               (gen_random_uuid(),'Biłgoraj',cet_guid,ST_GeomFromText('POINT (50.55 22.73)', 4326),'Poland'),
               (gen_random_uuid(),'Kłodzko',cet_guid,ST_GeomFromText('POINT (50.44 16.65)', 4326),'Poland'),
               (gen_random_uuid(),'Luboń',cet_guid,ST_GeomFromText('POINT (52.33 16.88)', 4326),'Poland'),
               (gen_random_uuid(),'Brzozów',cet_guid,ST_GeomFromText('POINT (49.7 22.02)', 4326),'Poland'),
               (gen_random_uuid(),'Wałcz',cet_guid,ST_GeomFromText('POINT (53.28 16.47)', 4326),'Poland'),
               (gen_random_uuid(),'Kluczbork',cet_guid,ST_GeomFromText('POINT (50.98 18.22)', 4326),'Poland'),
               (gen_random_uuid(),'Jarocin',cet_guid,ST_GeomFromText('POINT (51.97 17.5)', 4326),'Poland'),
               (gen_random_uuid(),'Świecie',cet_guid,ST_GeomFromText('POINT (53.42 18.43)', 4326),'Poland'),
               (gen_random_uuid(),'Wągrowiec',cet_guid,ST_GeomFromText('POINT (52.8 17.2)', 4326),'Poland'),
               (gen_random_uuid(),'Skawina',cet_guid,ST_GeomFromText('POINT (49.98 19.83)', 4326),'Poland'),
               (gen_random_uuid(),'Sandomierz',cet_guid,ST_GeomFromText('POINT (50.68 21.75)', 4326),'Poland'),
               (gen_random_uuid(),'Białogard',cet_guid,ST_GeomFromText('POINT (54.01 15.99)', 4326),'Poland'),
               (gen_random_uuid(),'Kościan',cet_guid,ST_GeomFromText('POINT (52.08 16.65)', 4326),'Poland'),
               (gen_random_uuid(),'Kościerzyna',cet_guid,ST_GeomFromText('POINT (54.12 17.98)', 4326),'Poland'),
               (gen_random_uuid(),'Szczytno',cet_guid,ST_GeomFromText('POINT (53.56 20.99)', 4326),'Poland'),
               (gen_random_uuid(),'Ostrów Mazowiecka',cet_guid,ST_GeomFromText('POINT (52.8 21.9)', 4326),'Poland'),
               (gen_random_uuid(),'Jawor',cet_guid,ST_GeomFromText('POINT (51.05 16.18)', 4326),'Poland'),
               (gen_random_uuid(),'Goleniów',cet_guid,ST_GeomFromText('POINT (53.56 14.83)', 4326),'Poland'),
               (gen_random_uuid(),'Lubartów',cet_guid,ST_GeomFromText('POINT (51.47 22.6)', 4326),'Poland'),
               (gen_random_uuid(),'Świedbodzin',cet_guid,ST_GeomFromText('POINT (52.25 15.53)', 4326),'Poland'),
               (gen_random_uuid(),'Grajewo',cet_guid,ST_GeomFromText('POINT (53.65 22.45)', 4326),'Poland'),
               (gen_random_uuid(),'Wieliczka',cet_guid,ST_GeomFromText('POINT (49.99 20.07)', 4326),'Poland'),
               (gen_random_uuid(),'Łęczna',cet_guid,ST_GeomFromText('POINT (51.3 22.88)', 4326),'Poland'),
               (gen_random_uuid(),'Opoczno',cet_guid,ST_GeomFromText('POINT (51.38 20.28)', 4326),'Poland'),
               (gen_random_uuid(),'Koło',cet_guid,ST_GeomFromText('POINT (52.2 18.63)', 4326),'Poland'),
               (gen_random_uuid(),'Działdowo',cet_guid,ST_GeomFromText('POINT (53.24 20.17)', 4326),'Poland'),
               (gen_random_uuid(),'Prudnik',cet_guid,ST_GeomFromText('POINT (50.32 17.58)', 4326),'Poland'),
               (gen_random_uuid(),'Gryfino',cet_guid,ST_GeomFromText('POINT (53.25 14.49)', 4326),'Poland'),
               (gen_random_uuid(),'Hajnówka',cet_guid,ST_GeomFromText('POINT (52.73 23.57)', 4326),'Poland'),
               (gen_random_uuid(),'Chełmno',cet_guid,ST_GeomFromText('POINT (53.35 18.43)', 4326),'Poland'),
               (gen_random_uuid(),'Pleszew',cet_guid,ST_GeomFromText('POINT (51.9 17.79)', 4326),'Poland'),
               (gen_random_uuid(),'Gostyń',cet_guid,ST_GeomFromText('POINT (51.88 17.01)', 4326),'Poland'),
               (gen_random_uuid(),'Nakło nad Notecią',cet_guid,ST_GeomFromText('POINT (53.14 17.6)', 4326),'Poland'),
               (gen_random_uuid(),'Pułtusk',cet_guid,ST_GeomFromText('POINT (52.7 21.08)', 4326),'Poland'),
               (gen_random_uuid(),'Chodzież',cet_guid,ST_GeomFromText('POINT (52.99 16.91)', 4326),'Poland'),
               (gen_random_uuid(),'Krasnystaw',cet_guid,ST_GeomFromText('POINT (51 23.17)', 4326),'Poland'),
               (gen_random_uuid(),'Gostynin',cet_guid,ST_GeomFromText('POINT (52.42 19.47)', 4326),'Poland'),
               (gen_random_uuid(),'Międzyrzecz',cet_guid,ST_GeomFromText('POINT (52.44 15.58)', 4326),'Poland'),
               (gen_random_uuid(),'Łask',cet_guid,ST_GeomFromText('POINT (51.59 19.13)', 4326),'Poland'),
               (gen_random_uuid(),'Oborniki',cet_guid,ST_GeomFromText('POINT (52.65 16.82)', 4326),'Poland'),
               (gen_random_uuid(),'Rawa Mazowiecka',cet_guid,ST_GeomFromText('POINT (51.77 20.25)', 4326),'Poland'),
               (gen_random_uuid(),'Łańcut',cet_guid,ST_GeomFromText('POINT (50.07 22.23)', 4326),'Poland'),
               (gen_random_uuid(),'Garwolin',cet_guid,ST_GeomFromText('POINT (51.9 21.62)', 4326),'Poland'),
               (gen_random_uuid(),'Kozienice',cet_guid,ST_GeomFromText('POINT (51.58 21.57)', 4326),'Poland'),
               (gen_random_uuid(),'Braniewo',cet_guid,ST_GeomFromText('POINT (54.38 19.83)', 4326),'Poland'),
               (gen_random_uuid(),'Bytów',cet_guid,ST_GeomFromText('POINT (54.17 17.5)', 4326),'Poland'),
               (gen_random_uuid(),'Brzesko',cet_guid,ST_GeomFromText('POINT (49.97 20.62)', 4326),'Poland'),
               (gen_random_uuid(),'Grójec',cet_guid,ST_GeomFromText('POINT (51.87 20.87)', 4326),'Poland'),
               (gen_random_uuid(),'Krapkowice',cet_guid,ST_GeomFromText('POINT (50.48 17.97)', 4326),'Poland'),
               (gen_random_uuid(),'Rypin',cet_guid,ST_GeomFromText('POINT (53.07 19.45)', 4326),'Poland'),
               (gen_random_uuid(),'Gryfice',cet_guid,ST_GeomFromText('POINT (53.92 15.2)', 4326),'Poland'),
               (gen_random_uuid(),'Olecko',cet_guid,ST_GeomFromText('POINT (54.03 22.5)', 4326),'Poland'),
               (gen_random_uuid(),'Złotoryja',cet_guid,ST_GeomFromText('POINT (51.13 15.92)', 4326),'Poland'),
               (gen_random_uuid(),'Przeworsk',cet_guid,ST_GeomFromText('POINT (50.06 22.49)', 4326),'Poland'),
               (gen_random_uuid(),'Łęczyca',cet_guid,ST_GeomFromText('POINT (52.06 19.2)', 4326),'Poland'),
               (gen_random_uuid(),'Jędrzejów',cet_guid,ST_GeomFromText('POINT (50.63 20.3)', 4326),'Poland'),
               (gen_random_uuid(),'Siemiatycze',cet_guid,ST_GeomFromText('POINT (52.43 22.86)', 4326),'Poland'),
               (gen_random_uuid(),'Choszczno',cet_guid,ST_GeomFromText('POINT (53.17 15.4)', 4326),'Poland'),
               (gen_random_uuid(),'Limanowa',cet_guid,ST_GeomFromText('POINT (49.72 20.47)', 4326),'Poland'),
               (gen_random_uuid(),'Grodzisk Wielkopolski',cet_guid,ST_GeomFromText('POINT (52.23 16.37)', 4326),'Poland'),
               (gen_random_uuid(),'Wschowa',cet_guid,ST_GeomFromText('POINT (51.8 16.3)', 4326),'Poland'),
               (gen_random_uuid(),'Nidzica',cet_guid,ST_GeomFromText('POINT (53.36 20.42)', 4326),'Poland'),
               (gen_random_uuid(),'Człuchów',cet_guid,ST_GeomFromText('POINT (53.65 17.37)', 4326),'Poland'),
               (gen_random_uuid(),'Gołdap',cet_guid,ST_GeomFromText('POINT (54.31 22.3)', 4326),'Poland'),
               (gen_random_uuid(),'Głubczyce',cet_guid,ST_GeomFromText('POINT (50.2 17.83)', 4326),'Poland'),
               (gen_random_uuid(),'Wolsztyn',cet_guid,ST_GeomFromText('POINT (52.12 16.12)', 4326),'Poland'),
               (gen_random_uuid(),'Trzebnica',cet_guid,ST_GeomFromText('POINT (51.3 17.06)', 4326),'Poland'),
               (gen_random_uuid(),'Włodawa',cet_guid,ST_GeomFromText('POINT (51.55 23.55)', 4326),'Poland'),
               (gen_random_uuid(),'Golub-Dobrzyń',cet_guid,ST_GeomFromText('POINT (53.1 19.05)', 4326),'Poland'),
               (gen_random_uuid(),'Brzeziny',cet_guid,ST_GeomFromText('POINT (51.8 19.75)', 4326),'Poland'),
               (gen_random_uuid(),'Lubaczów',cet_guid,ST_GeomFromText('POINT (50.16 23.12)', 4326),'Poland'),
               (gen_random_uuid(),'Aleksandrów Kujawski',cet_guid,ST_GeomFromText('POINT (52.88 18.7)', 4326),'Poland'),
               (gen_random_uuid(),'Węgorzewo',cet_guid,ST_GeomFromText('POINT (54.22 21.75)', 4326),'Poland'),
               (gen_random_uuid(),'Janów Lubelski',cet_guid,ST_GeomFromText('POINT (50.7 22.4)', 4326),'Poland'),
               (gen_random_uuid(),'Góra',cet_guid,ST_GeomFromText('POINT (51.67 16.55)', 4326),'Poland'),
               (gen_random_uuid(),'Drawsko Pomorskie',cet_guid,ST_GeomFromText('POINT (53.53 15.8)', 4326),'Poland'),
               (gen_random_uuid(),'Krosno Odrzańskie',cet_guid,ST_GeomFromText('POINT (52.05 15.1)', 4326),'Poland'),
               (gen_random_uuid(),'Nowe Miasto Lubawskie',cet_guid,ST_GeomFromText('POINT (53.42 19.58)', 4326),'Poland'),
               (gen_random_uuid(),'Kolno',cet_guid,ST_GeomFromText('POINT (53.41 21.93)', 4326),'Poland'),
               (gen_random_uuid(),'Lwówek Śląski',cet_guid,ST_GeomFromText('POINT (51.12 15.58)', 4326),'Poland'),
               (gen_random_uuid(),'Kamień Pomorski',cet_guid,ST_GeomFromText('POINT (53.97 14.79)', 4326),'Poland'),
               (gen_random_uuid(),'Kolbuszowa',cet_guid,ST_GeomFromText('POINT (50.25 21.77)', 4326),'Poland'),
               (gen_random_uuid(),'Kazimierza Wielka',cet_guid,ST_GeomFromText('POINT (50.27 20.49)', 4326),'Poland'),
               (gen_random_uuid(),'Hrubieszów',cet_guid,ST_GeomFromText('POINT (50.82 23.88)', 4326),'Poland'),
               (gen_random_uuid(),'Świętochłowice',cet_guid,ST_GeomFromText('POINT (50.3 18.91)', 4326),'Poland'),
               (gen_random_uuid(),'Lubin',cet_guid,ST_GeomFromText('POINT (51.4 16.2)', 4326),'Poland'),
               (gen_random_uuid(),'Mińsk Mazowiecki',cet_guid,ST_GeomFromText('POINT (52.18 21.57)', 4326),'Poland'),
               (gen_random_uuid(),'Żyrardów',cet_guid,ST_GeomFromText('POINT (52.05 20.43)', 4326),'Poland'),
               (gen_random_uuid(),'Tczew',cet_guid,ST_GeomFromText('POINT (54.09 18.79)', 4326),'Poland'),
               (gen_random_uuid(),'Ostróda',cet_guid,ST_GeomFromText('POINT (53.7 19.97)', 4326),'Poland'),
               (gen_random_uuid(),'Siemianowice Śląskie',cet_guid,ST_GeomFromText('POINT (50.33 19.03)', 4326),'Poland'),
               (gen_random_uuid(),'Puck',cet_guid,ST_GeomFromText('POINT (54.71 18.41)', 4326),'Poland'),
               (gen_random_uuid(),'Wejherowo',cet_guid,ST_GeomFromText('POINT (54.6 18.25)', 4326),'Poland'),
               (gen_random_uuid(),'Sokołów Podlaski',cet_guid,ST_GeomFromText('POINT (52.41 22.25)', 4326),'Poland'),
               (gen_random_uuid(),'Płońsk',cet_guid,ST_GeomFromText('POINT (52.62 20.38)', 4326),'Poland'),
               (gen_random_uuid(),'Wąbrzeźno',cet_guid,ST_GeomFromText('POINT (53.28 18.95)', 4326),'Poland'),
               (gen_random_uuid(),'Mrągowo',cet_guid,ST_GeomFromText('POINT (53.86 21.31)', 4326),'Poland'),
               (gen_random_uuid(),'Pruszcz Gdański',cet_guid,ST_GeomFromText('POINT (54.26 18.64)', 4326),'Poland'),
               (gen_random_uuid(),'Zduńska Wola',cet_guid,ST_GeomFromText('POINT (51.6 18.93)', 4326),'Poland'),
               (gen_random_uuid(),'Oleśnica',cet_guid,ST_GeomFromText('POINT (51.2 17.38)', 4326),'Poland'),
               (gen_random_uuid(),'Ostrów Wielkopolski',cet_guid,ST_GeomFromText('POINT (51.65 17.7)', 4326),'Poland'),
               (gen_random_uuid(),'Turek',cet_guid,ST_GeomFromText('POINT (52.02 18.5)', 4326),'Poland'),
               (gen_random_uuid(),'Złotów',cet_guid,ST_GeomFromText('POINT (53.36 17.04)', 4326),'Poland'),
               (gen_random_uuid(),'Tomaszów Mazowiecki',cet_guid,ST_GeomFromText('POINT (51.53 20.01)', 4326),'Poland'),
               (gen_random_uuid(),'Tomaszów Lubelski',cet_guid,ST_GeomFromText('POINT (50.45 23.42)', 4326),'Poland'),
               (gen_random_uuid(),'Nowy Sącz',cet_guid,ST_GeomFromText('POINT (49.62 20.7)', 4326),'Poland'),
               (gen_random_uuid(),'Sochaczew',cet_guid,ST_GeomFromText('POINT (52.22 20.23)', 4326),'Poland'),
               (gen_random_uuid(),'Piekary Śląskie',cet_guid,ST_GeomFromText('POINT (50.38 18.95)', 4326),'Poland'),
               (gen_random_uuid(),'Stargard Szczeciński',cet_guid,ST_GeomFromText('POINT (53.34 15.04)', 4326),'Poland'),
               (gen_random_uuid(),'Lipno',cet_guid,ST_GeomFromText('POINT (52.85 19.18)', 4326),'Poland'),
               (gen_random_uuid(),'Zgierz',cet_guid,ST_GeomFromText('POINT (51.86 19.41)', 4326),'Poland'),
               (gen_random_uuid(),'Lubań',cet_guid,ST_GeomFromText('POINT (51.12 15.3)', 4326),'Poland'),
               (gen_random_uuid(),'Nowy Dwór Mazowiecki',cet_guid,ST_GeomFromText('POINT (52.43 20.68)', 4326),'Poland'),
               (gen_random_uuid(),'Zambrów',cet_guid,ST_GeomFromText('POINT (52.99 22.24)', 4326),'Poland'),
               (gen_random_uuid(),'Szczecinek',cet_guid,ST_GeomFromText('POINT (53.71 16.7)', 4326),'Poland'),
               (gen_random_uuid(),'Sejny',cet_guid,ST_GeomFromText('POINT (54.1 23.35)', 4326),'Poland'),
               (gen_random_uuid(),'Oława',cet_guid,ST_GeomFromText('POINT (50.93 17.3)', 4326),'Poland'),
               (gen_random_uuid(),'Czarnków',cet_guid,ST_GeomFromText('POINT (52.9 16.57)', 4326),'Poland'),
               (gen_random_uuid(),'Lidzbark Warmiński',cet_guid,ST_GeomFromText('POINT (54.12 20.58)', 4326),'Poland'),
               (gen_random_uuid(),'Kamienna Góra',cet_guid,ST_GeomFromText('POINT (50.78 16.03)', 4326),'Poland'),
               (gen_random_uuid(),'Słupca',cet_guid,ST_GeomFromText('POINT (52.3 17.87)', 4326),'Poland'),
               (gen_random_uuid(),'Jastrzębie-Zdrój',cet_guid,ST_GeomFromText('POINT (49.95 18.58)', 4326),'Poland'),
               (gen_random_uuid(),'Krosno',cet_guid,ST_GeomFromText('POINT (49.69 21.77)', 4326),'Poland'),
               (gen_random_uuid(),'Skarżysko-Kamienna',cet_guid,ST_GeomFromText('POINT (51.12 20.92)', 4326),'Poland'),
               (gen_random_uuid(),'Sanok',cet_guid,ST_GeomFromText('POINT (49.55 22.22)', 4326),'Poland'),
               (gen_random_uuid(),'Radziejów',cet_guid,ST_GeomFromText('POINT (52.63 18.52)', 4326),'Poland'),
               (gen_random_uuid(),'Bartoszyce',cet_guid,ST_GeomFromText('POINT (54.25 20.81)', 4326),'Poland'),
               (gen_random_uuid(),'Maków Mazowiecki',cet_guid,ST_GeomFromText('POINT (52.86 21.1)', 4326),'Poland'),
               (gen_random_uuid(),'Sierpc',cet_guid,ST_GeomFromText('POINT (52.88 19.67)', 4326),'Poland'),
               (gen_random_uuid(),'Wodzisław Śląski',cet_guid,ST_GeomFromText('POINT (50 18.45)', 4326),'Poland'),
               (gen_random_uuid(),'Żory',cet_guid,ST_GeomFromText('POINT (50.05 18.7)', 4326),'Poland'),
               (gen_random_uuid(),'Radomsko',cet_guid,ST_GeomFromText('POINT (51.07 19.45)', 4326),'Poland'),
               (gen_random_uuid(),'Wołomin',cet_guid,ST_GeomFromText('POINT (52.35 21.23)', 4326),'Poland'),
               (gen_random_uuid(),'Radzyń Podlaski',cet_guid,ST_GeomFromText('POINT (51.78 22.62)', 4326),'Poland'),
               (gen_random_uuid(),'Sławno',cet_guid,ST_GeomFromText('POINT (54.37 16.68)', 4326),'Poland'),
               (gen_random_uuid(),'Stalowa Wola',cet_guid,ST_GeomFromText('POINT (50.58 22.05)', 4326),'Poland'),
               (gen_random_uuid(),'Racibórz',cet_guid,ST_GeomFromText('POINT (50.09 18.22)', 4326),'Poland'),
               (gen_random_uuid(),'Piła',cet_guid,ST_GeomFromText('POINT (53.15 16.73)', 4326),'Poland'),
               (gen_random_uuid(),'Żywiec',cet_guid,ST_GeomFromText('POINT (49.71 19.21)', 4326),'Poland'),
               (gen_random_uuid(),'Świdwin',cet_guid,ST_GeomFromText('POINT (53.77 15.78)', 4326),'Poland'),
               (gen_random_uuid(),'Przasnysz',cet_guid,ST_GeomFromText('POINT (53.02 20.88)', 4326),'Poland'),
               (gen_random_uuid(),'Żagań',cet_guid,ST_GeomFromText('POINT (51.62 15.32)', 4326),'Poland'),
               (gen_random_uuid(),'Piaseczno',cet_guid,ST_GeomFromText('POINT (52.07 21.02)', 4326),'Poland'),
               (gen_random_uuid(),'Wysokie Mazowieckie',cet_guid,ST_GeomFromText('POINT (52.92 22.51)', 4326),'Poland'),
               (gen_random_uuid(),'Leżajsk',cet_guid,ST_GeomFromText('POINT (50.27 22.42)', 4326),'Poland'),
               (gen_random_uuid(),'Zawiercie',cet_guid,ST_GeomFromText('POINT (50.49 19.42)', 4326),'Poland'),
               (gen_random_uuid(),'Kędzierzyn-Koźle',cet_guid,ST_GeomFromText('POINT (50.33 18.2)', 4326),'Poland'),
               (gen_random_uuid(),'Bieruń',cet_guid,ST_GeomFromText('POINT (50.09 19.09)', 4326),'Poland'),
               (gen_random_uuid(),'Myszków',cet_guid,ST_GeomFromText('POINT (50.58 19.32)', 4326),'Poland'),
               (gen_random_uuid(),'Słupsk',cet_guid,ST_GeomFromText('POINT (54.47 17.03)', 4326),'Poland'),
               (gen_random_uuid(),'Węgrów',cet_guid,ST_GeomFromText('POINT (52.4 22.02)', 4326),'Poland'),
               (gen_random_uuid(),'Wadowice',cet_guid,ST_GeomFromText('POINT (49.88 19.5)', 4326),'Poland'),
               (gen_random_uuid(),'Olkusz',cet_guid,ST_GeomFromText('POINT (50.28 19.57)', 4326),'Poland'),
               (gen_random_uuid(),'Sucha Beskidzka',cet_guid,ST_GeomFromText('POINT (49.74 19.59)', 4326),'Poland'),
               (gen_random_uuid(),'Ożarów Mazowiecki',cet_guid,ST_GeomFromText('POINT (52.22 20.8)', 4326),'Poland'),
               (gen_random_uuid(),'Pszczyna',cet_guid,ST_GeomFromText('POINT (49.98 18.95)', 4326),'Poland'),
               (gen_random_uuid(),'Myślenice',cet_guid,ST_GeomFromText('POINT (49.83 19.93)', 4326),'Poland'),
               (gen_random_uuid(),'Lubliniec',cet_guid,ST_GeomFromText('POINT (50.67 18.68)', 4326),'Poland'),
               (gen_random_uuid(),'Wieluń',cet_guid,ST_GeomFromText('POINT (51.22 18.57)', 4326),'Poland'),
               (gen_random_uuid(),'Wyszków',cet_guid,ST_GeomFromText('POINT (52.59 21.46)', 4326),'Poland'),
               (gen_random_uuid(),'Rawicz',cet_guid,ST_GeomFromText('POINT (51.61 16.86)', 4326),'Poland'),
               (gen_random_uuid(),'Września',cet_guid,ST_GeomFromText('POINT (52.33 17.58)', 4326),'Poland'),
               (gen_random_uuid(),'Śrem',cet_guid,ST_GeomFromText('POINT (52.08 17.02)', 4326),'Poland'),
               (gen_random_uuid(),'Kępno',cet_guid,ST_GeomFromText('POINT (51.28 17.99)', 4326),'Poland'),
               (gen_random_uuid(),'Ropczyce',cet_guid,ST_GeomFromText('POINT (50.08 21.63)', 4326),'Poland'),
               (gen_random_uuid(),'Dąbrowa Tarnowska',cet_guid,ST_GeomFromText('POINT (50.18 20.98)', 4326),'Poland'),
               (gen_random_uuid(),'Polkowice',cet_guid,ST_GeomFromText('POINT (51.5 16.07)', 4326),'Poland'),
               (gen_random_uuid(),'Kartuzy',cet_guid,ST_GeomFromText('POINT (54.33 18.2)', 4326),'Poland'),
               (gen_random_uuid(),'Szamotuły',cet_guid,ST_GeomFromText('POINT (52.6 16.58)', 4326),'Poland'),
               (gen_random_uuid(),'Proszowice',cet_guid,ST_GeomFromText('POINT (50.2 20.3)', 4326),'Poland'),
               (gen_random_uuid(),'Kłobuck',cet_guid,ST_GeomFromText('POINT (50.9 18.94)', 4326),'Poland'),
               (gen_random_uuid(),'Nisko',cet_guid,ST_GeomFromText('POINT (50.53 22.13)', 4326),'Poland'),
               (gen_random_uuid(),'Ząbkowice Śląskie',cet_guid,ST_GeomFromText('POINT (50.59 16.81)', 4326),'Poland'),
               (gen_random_uuid(),'Strzelce Opolskie',cet_guid,ST_GeomFromText('POINT (50.5 18.28)', 4326),'Poland'),
               (gen_random_uuid(),'Strzyżów',cet_guid,ST_GeomFromText('POINT (49.88 21.78)', 4326),'Poland'),
               (gen_random_uuid(),'Środa Wielkopolska',cet_guid,ST_GeomFromText('POINT (52.23 17.28)', 4326),'Poland'),
               (gen_random_uuid(),'Wieruszów',cet_guid,ST_GeomFromText('POINT (51.3 18.15)', 4326),'Poland'),
               (gen_random_uuid(),'Końskie',cet_guid,ST_GeomFromText('POINT (51.2 20.42)', 4326),'Poland'),
               (gen_random_uuid(),'Busko-Zdrój',cet_guid,ST_GeomFromText('POINT (50.47 20.72)', 4326),'Poland'),
               (gen_random_uuid(),'Nowy Tomyśl',cet_guid,ST_GeomFromText('POINT (52.32 16.13)', 4326),'Poland'),
               (gen_random_uuid(),'Białobrzegi',cet_guid,ST_GeomFromText('POINT (51.65 20.95)', 4326),'Poland'),
               (gen_random_uuid(),'Miechów',cet_guid,ST_GeomFromText('POINT (50.36 20.03)', 4326),'Poland'),
               (gen_random_uuid(),'Ryki',cet_guid,ST_GeomFromText('POINT (51.63 21.93)', 4326),'Poland'),
               (gen_random_uuid(),'Ostrzeszów',cet_guid,ST_GeomFromText('POINT (51.4 18)', 4326),'Poland'),
               (gen_random_uuid(),'Strzelin',cet_guid,ST_GeomFromText('POINT (50.78 17.07)', 4326),'Poland'),
               (gen_random_uuid(),'Szydłowiec',cet_guid,ST_GeomFromText('POINT (51.23 20.86)', 4326),'Poland'),
               (gen_random_uuid(),'Staszów',cet_guid,ST_GeomFromText('POINT (50.56 21.17)', 4326),'Poland'),
               (gen_random_uuid(),'Żuromin',cet_guid,ST_GeomFromText('POINT (53.07 19.91)', 4326),'Poland'),
               (gen_random_uuid(),'Opatów',cet_guid,ST_GeomFromText('POINT (50.8 21.42)', 4326),'Poland'),
               (gen_random_uuid(),'Sztum',cet_guid,ST_GeomFromText('POINT (53.92 19.03)', 4326),'Poland'),
               (gen_random_uuid(),'Słubice',cet_guid,ST_GeomFromText('POINT (52.35 14.57)', 4326),'Poland'),
               (gen_random_uuid(),'Lesko',cet_guid,ST_GeomFromText('POINT (49.47 22.33)', 4326),'Poland'),
               (gen_random_uuid(),'Żnin',cet_guid,ST_GeomFromText('POINT (52.85 17.72)', 4326),'Poland'),
               (gen_random_uuid(),'Pajęczno',cet_guid,ST_GeomFromText('POINT (51.15 19)', 4326),'Poland'),
               (gen_random_uuid(),'Parczew',cet_guid,ST_GeomFromText('POINT (51.63 22.87)', 4326),'Poland'),
               (gen_random_uuid(),'Mogilno',cet_guid,ST_GeomFromText('POINT (52.66 17.95)', 4326),'Poland'),
               (gen_random_uuid(),'Pińczów',cet_guid,ST_GeomFromText('POINT (50.52 20.53)', 4326),'Poland'),
               (gen_random_uuid(),'Pyrzyce',cet_guid,ST_GeomFromText('POINT (53.14 14.89)', 4326),'Poland'),
               (gen_random_uuid(),'Mońki',cet_guid,ST_GeomFromText('POINT (53.41 22.8)', 4326),'Poland'),
               (gen_random_uuid(),'Środa Śląska',cet_guid,ST_GeomFromText('POINT (51.15 16.58)', 4326),'Poland'),
               (gen_random_uuid(),'Opole Lubelskie',cet_guid,ST_GeomFromText('POINT (51.15 21.97)', 4326),'Poland'),
               (gen_random_uuid(),'Łosice',cet_guid,ST_GeomFromText('POINT (52.22 22.72)', 4326),'Poland'),
               (gen_random_uuid(),'Zwoleń',cet_guid,ST_GeomFromText('POINT (51.36 21.59)', 4326),'Poland'),
               (gen_random_uuid(),'Namysłów',cet_guid,ST_GeomFromText('POINT (51.07 17.71)', 4326),'Poland'),
               (gen_random_uuid(),'Nowy Dwór Gdański',cet_guid,ST_GeomFromText('POINT (54.22 19.12)', 4326),'Poland'),
               (gen_random_uuid(),'Tuchola',cet_guid,ST_GeomFromText('POINT (53.59 17.86)', 4326),'Poland'),
               (gen_random_uuid(),'Lipsko',cet_guid,ST_GeomFromText('POINT (51.16 21.65)', 4326),'Poland'),
               (gen_random_uuid(),'Sokółka',cet_guid,ST_GeomFromText('POINT (53.4 23.5)', 4326),'Poland'),
               (gen_random_uuid(),'Włoszczowa',cet_guid,ST_GeomFromText('POINT (50.85 19.97)', 4326),'Poland'),
               (gen_random_uuid(),'Olesno',cet_guid,ST_GeomFromText('POINT (50.88 18.42)', 4326),'Poland'),
               (gen_random_uuid(),'Sępólno Krajeńskie',cet_guid,ST_GeomFromText('POINT (53.45 17.53)', 4326),'Poland'),
               (gen_random_uuid(),'Poddębice',cet_guid,ST_GeomFromText('POINT (51.9 18.97)', 4326),'Poland'),
               (gen_random_uuid(),'Przysucha',cet_guid,ST_GeomFromText('POINT (51.36 20.63)', 4326),'Poland'),
               (gen_random_uuid(),'Wołów',cet_guid,ST_GeomFromText('POINT (51.34 16.63)', 4326),'Poland'),
               (gen_random_uuid(),'Myślibórz',cet_guid,ST_GeomFromText('POINT (52.92 14.87)', 4326),'Poland'),
               (gen_random_uuid(),'Łobez',cet_guid,ST_GeomFromText('POINT (53.64 15.62)', 4326),'Poland'),
               (gen_random_uuid(),'Milicz',cet_guid,ST_GeomFromText('POINT (51.53 17.28)', 4326),'Poland'),
               (gen_random_uuid(),'Strzelce Krajeńskie',cet_guid,ST_GeomFromText('POINT (52.88 15.52)', 4326),'Poland'),
               (gen_random_uuid(),'Pisz',cet_guid,ST_GeomFromText('POINT (53.63 21.81)', 4326),'Poland'),
               (gen_random_uuid(),'Sulęcin',cet_guid,ST_GeomFromText('POINT (52.45 15.12)', 4326),'Poland'),
               (gen_random_uuid(),'Ustrzyki Dolne',cet_guid,ST_GeomFromText('POINT (49.43 22.6)', 4326),'Poland'),
               (gen_random_uuid(),'Międzychód',cet_guid,ST_GeomFromText('POINT (52.6 15.89)', 4326),'Poland');

    END
$$;
