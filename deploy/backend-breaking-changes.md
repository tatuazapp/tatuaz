## TA-144

---

Ze względu na wykorzystanie danych typu Point potrzebne jest rozszerzenie postgis do postgresa. Tilt i chmura używają obrazu od razu z tym rozszerzeniem jednak lokalnie trzeba pobrać to rozszerzenie z "Application Stack Builder"(Jeżeli macie postgresa to powinno być zainstalowane). Ze względu na błąd w schemie usunąłem wszystkie migracje i wykonałem je od nowa(nie mieliśmy istotnych danych w bazie, więcej tak już raczej nie będę robił), więc przed normalnym <code>dotnet ef database update...</code> robimy <code>dotnet ef database drop...</code> z tymi samymi parametrami.
