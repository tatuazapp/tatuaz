﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"INSERT INTO ""photo"".""categories""(title, type, image_uri)
VALUES ('Czaszki', 1, '/tatuaz-images/12e44657cafb4c2bb858609c6f573660.jpg'),
('Kaktusy', 1, '/tatuaz-images/986500a2a6e346639b882500862ca833.jpg'),
('Koła', 1, '/tatuaz-images/dc54f3092746427391484cc8661c0fbe.jpg'),
('Koty', 1, '/tatuaz-images/78a919aed4fc4b1dbeb1d5c2fd8a2f09.jpg'),
('Księżyce', 1, '/tatuaz-images/37f43c8a1a264c49bc3f3ec78cd00d04.jpg'),
('Kwiaty', 1, '/tatuaz-images/b9c3603a307c43fb870a075b1d753cc0.jpg'),
('Lamparty', 1, '/tatuaz-images/d65c6e058bbe49eba4a08261fee28dd0.jpg'),
('Lisy', 1, '/tatuaz-images/09ce63d17e9447c29e0782b5cdef7083.jpg'),
('Łapki', 1, '/tatuaz-images/a23673201f5143acab93dc31af586b7a.jpg'),
('Motyle', 1, '/tatuaz-images/4ff7c3a64da448d19c709b6406e65a95.jpg'),
('Napisy', 1, '/tatuaz-images/31c900b4bbb54d2782ed192664e3b027.jpg'),
('Pióra', 1, '/tatuaz-images/a3dbb5c445884b97b30d2799691dc94f.jpg'),
('Postacie', 1, '/tatuaz-images/a4eaed9de45445e1a55cc1886ed4b58f.jpg'),
('Rośliny', 1, '/tatuaz-images/89e404b1a7f445068714b3ae175fc3b2.jpg'),
('Serca', 1, '/tatuaz-images/c8e9a0dfb74e49f5962deaa7a2cf049b.jpg'),
('Skorpiony', 1, '/tatuaz-images/610e0de80efc42e0b6b7dec9d7b95252.jpg'),
('Słoneczniki', 1, '/tatuaz-images/3f84ad7178e24579bc32356b3a432e13.jpg'),
('Słońca', 1, '/tatuaz-images/3ce5878cb0c642338a23c58a0bdd23cf.jpg'),
('Smoki', 1, '/tatuaz-images/7e0055ccbd83423097a30da0f44d37c0.jpg'),
('Symbole', 1, '/tatuaz-images/c6aafedb4b244ba5aeafb77063f1425c.jpg'),
('Tygrysy', 1, '/tatuaz-images/4a86937f1943455aac286cbee43b8c15.jpg'),
('Węże', 1, '/tatuaz-images/17a46a589ba84e838dd5c522a330fef1.jpg'),
('Wilki', 1, '/tatuaz-images/17e5a4618a0d46f39274d27ccc957e05.jpg'),
('Yin i Yang', 1, '/tatuaz-images/d684d641f9244384af5f2bae71126bef.jpg'),
('Biodro', 2, '/tatuaz-images/9e9d66cfdc2e4c4887c64b5657fe9f29.jpg'),
('Brzuch', 2, '/tatuaz-images/ac8306d019db4342ac1a9d370b5caa8c.jpg'),
('Dłoń', 2, '/tatuaz-images/0df8162b88dc400bad353f093c9f6788.jpg'),
('Głowa', 2, '/tatuaz-images/ed8259161af347eb9bb6be43f7b01bc6.jpg'),
('Kark', 2, '/tatuaz-images/0464cd69ef9c44f7aff253cd249bfe63.jpg'),
('Klatka piersiowa', 2, '/tatuaz-images/2a60ef81cf414b27aee5a7e227449ab5.jpg'),
('Kostka', 2, '/tatuaz-images/2ad9fccc01db44a09ca14185f470dd3e.jpg'),
('Łopatka', 2, '/tatuaz-images/9361a1f6578245a6816d73b0dc41dbdf.jpg'),
('Łydka', 2, '/tatuaz-images/a8b66cb00adc474f99aa6641b53c8555.jpg'),
('Mostek', 2, '/tatuaz-images/4edcb0f7d8eb482a862dbbe2a1b28160.jpg'),
('Nadgarstek', 2, '/tatuaz-images/d918be12ce5d416f9bcce2adcbb7e3c9.jpg'),
('Obojczyk', 2, '/tatuaz-images/ec71b9e7b4ac4d64a32a9a5555c51a8e.jpg'),
('Palce u rąk', 2, '/tatuaz-images/1f91edf2ba82431a8b350c24762ec38c.jpg'),
('Plecy', 2, '/tatuaz-images/b3ba6ec581b4488dbf73ca264c0a7ce2.jpg'),
('Przedramię', 2, '/tatuaz-images/907600a320d4428fa127d514d8029dbb.jpg'),
('Ramię', 2, '/tatuaz-images/17090315584e430fa42ba1eb6788c54e.jpg'),
('Stopa', 2, '/tatuaz-images/d812accf5cb342938ac9ce0e8e2ffb6d.jpg'),
('Szyja', 2, '/tatuaz-images/2a2b45292aa84d45a86c9cf2c1117a44.jpg'),
('Twarz', 2, '/tatuaz-images/56619feb59764168b80628dee6e28104.jpg'),
('Tyłek', 2, '/tatuaz-images/19042ff478da4de3a30a4d5e0ff8bbd0.jpg'),
('Ucho', 2, '/tatuaz-images/ed8faebab9d14ae1bf3688693c89d61f.jpg'),
('Udo', 2, '/tatuaz-images/a2ad6e8949e34ef7b9c75244a9ea9e76.jpg'),
('Żebro', 2, '/tatuaz-images/8504e81a202f45af80335fa2e8b7da0b.jpg'),
('Linework', 0, '/tatuaz-images/0b6b256c0b2548e9ac202c0a5f8a904b.jpg'),
('Geometryczny', 0, '/tatuaz-images/707cf65955e246e9a53832785e318b80.jpg'),
('Woodcut', 0, '/tatuaz-images/0b46a1993d8f4622acf14c8c8010e744.jpg'),
('Ignorant', 0, '/tatuaz-images/c0bcd65e890142b9964f79f94b5b2d45.jpg'),
('Watercolor', 0, '/tatuaz-images/f1b1a9de5fb944128460bba3916638b4.jpg'),
('Minimalist', 0, '/tatuaz-images/3ea751ccb24c47109c3663377f0fb105.jpg'),
('Dotwork', 0, '/tatuaz-images/9834bb694142408ab8624b846697144f.jpg');"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"photos\".\"Categories\";");
        }
    }
}
