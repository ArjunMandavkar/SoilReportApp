using Microsoft.EntityFrameworkCore;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Enums;
using SoilReportApp.Domain.Interfaces.Repositories;
using SoilReportApp.Infrastructure.Data;

namespace SoilReportApp.Infrastructure.Repositories;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ApplicationDbContext _context;

    public DatabaseInitializer(ApplicationDbContext context)
    {
        _context = context;
    }

    public void InitializeDatabase()
    {
        _context.Database.Migrate(); // Apply pending migrations

        if (!_context.Users.Any()) // Avoid duplicate seeding
        {
            _context.Users.AddRange(
                new User {
                        Id = new Guid("bc5bafd3-4d45-42cf-b087-4b897e9e212c"),
                        Email = "admin@gmail.com",
                        Username = "admin",
                        Password = "welcome1",
                        Phone = "0123456789",
                        UserType = UserType.Expert,
                        DeviceId = 0
                });
            _context.SaveChanges();
        }

        if (!_context.Crops.Any())
        {
            _context.Crops.AddRange(
                new Crop()
                {
                    Id = new Guid("fade32a1-5aea-4d25-bb11-9258b0219436"),
                    Name = "Almond"
                },
                new Crop()
                {
                    Id = new Guid("4a8968c4-bc51-4240-b4d2-dbe7ecbdd918"),
                    Name = "Amaranth"
                },
                new Crop()
                {
                    Id = new Guid("4040b9a9-00f9-462c-a2d3-69f2fb201c71"),
                    Name = "Apple"
                },
                new Crop()
                {
                    Id = new Guid("c14935c8-bb38-4e7f-9b61-71f078d880b4"),
                    Name = "Apricot"
                },
                new Crop()
                {
                    Id = new Guid("b13b3d44-0166-400d-aee2-42dcfb9d2489"),
                    Name = "Arecanut"
                },
                new Crop()
                {
                    Id = new Guid("2ad8aed2-7ee3-45c5-ae4d-3dc2131105e0"),
                    Name = "Ash Gourd"
                },
                new Crop()
                {
                    Id = new Guid("3535f7f4-a702-4ee5-b887-c334ac0588c9"),
                    Name = "Asparagus"
                },
                new Crop()
                {
                    Id = new Guid("a7dbbd10-7677-40ca-ad63-29e59ef214cf"),
                    Name = "Avocado"
                },
                new Crop()
                {
                    Id = new Guid("4af37e77-1002-4d5e-a6d8-b6885f0ac5dc"),
                    Name = "Banana"
                },
                new Crop()
                {
                    Id = new Guid("2afd0ac1-ea09-44f0-8995-f8d4f27422a2"),
                    Name = "Barley"
                },
                new Crop()
                {
                    Id = new Guid("439fbb4f-bc9d-40be-99fd-d3ee28e40acb"),
                    Name = "Basil"
                },
                new Crop()
                {
                    Id = new Guid("f68d3726-3a8d-4a40-95f8-d6e5a9e49203"),
                    Name = "Beetroot"
                },
                new Crop()
                {
                    Id = new Guid("c3b47f57-9521-48e2-bc81-1aee5c843b4a"),
                    Name = "Ber (Indian Jujube)"
                },
                new Crop()
                {
                    Id = new Guid("38dd0b98-c5f6-4aa5-babf-f6286794cded"),
                    Name = "Betel Leaf"
                },
                new Crop()
                {
                    Id = new Guid("422bdbf5-27c9-49f1-8078-3f9892f1b4cc"),
                    Name = "Black Gram (Urad Dal)"
                },
                new Crop()
                {
                    Id = new Guid("89784da6-051c-48b9-b9a2-f9e62aa36876"),
                    Name = "Bottle Gourd"
                },
                new Crop()
                {
                    Id = new Guid("e9b53f2a-36ac-4981-bf32-feab89b690a3"),
                    Name = "Brinjal (Eggplant)"
                },
                new Crop()
                {
                    Id = new Guid("1ee74260-453e-4c2d-bd2d-50d27fe9f13a"),
                    Name = "Broccoli"
                },
                new Crop()
                {
                    Id = new Guid("af1fbc4d-32f2-449b-a208-69063df25bc2"),
                    Name = "Buckwheat"
                },
                new Crop()
                {
                    Id = new Guid("5ca0d955-9f70-420b-8aa5-7d095df14f6c"),
                    Name = "Cabbage"
                },
                new Crop()
                {
                    Id = new Guid("afa0e9b3-badc-4e68-aefa-0b94a8a5f6fd"),
                    Name = "Capsicum"
                },
                new Crop()
                {
                    Id = new Guid("7045c00a-4345-4171-a140-e5bf45f5cf3a"),
                    Name = "Carrot"
                },
                new Crop()
                {
                    Id = new Guid("599f83b4-81f5-4cb9-beaf-8275b27c0234"),
                    Name = "Cashew Nut"
                },
                new Crop()
                {
                    Id = new Guid("39ae2a52-7e8f-47da-a39e-7c5d9c512942"),
                    Name = "Cassava (Tapioca)"
                },
                new Crop()
                {
                    Id = new Guid("4a550f8f-3ca9-4577-9ef6-28f927cd448a"),
                    Name = "Cauliflower"
                },
                new Crop()
                {
                    Id = new Guid("f67cb2a0-6301-4385-9e8f-a41a9023b77b"),
                    Name = "Chilli"
                },
                new Crop()
                {
                    Id = new Guid("239295a6-4ff3-4c7d-9fca-db4f744c50ba"),
                    Name = "Chickpea (Chana)"
                },
                new Crop()
                {
                    Id = new Guid("04a173e0-d82d-4339-ba1d-108c0b16ab81"),
                    Name = "Cinnamon"
                },
                new Crop()
                {
                    Id = new Guid("d1e9cab8-0f15-4330-ab94-67d1084992ca"),
                    Name = "Clove"
                },
                new Crop()
                {
                    Id = new Guid("5d5a11fb-c4bc-4fab-ba0a-a074ec751ec7"),
                    Name = "Coconut"
                },
                new Crop()
                {
                    Id = new Guid("34b2b41d-54ca-4560-b5f2-f464e5431263"),
                    Name = "Coffee"
                },
                new Crop()
                {
                    Id = new Guid("a9477ed5-88a8-468b-9a07-9fb4d8b79c69"),
                    Name = "Colocasia (Arbi)"
                },
                new Crop()
                {
                    Id = new Guid("b825d8f6-3495-4b55-84f1-b9401375acf8"),
                    Name = "Coriander"
                },
                new Crop()
                {
                    Id = new Guid("9ab06bfb-77d3-47ce-a102-f209ba7ac840"),
                    Name = "Cotton"
                },
                new Crop()
                {
                    Id = new Guid("65299dee-a0d7-4234-83b0-c002569610c0"),
                    Name = "Cucumber"
                },
                new Crop()
                {
                    Id = new Guid("414f10b3-2b64-42c9-9b3c-2c654abb8150"),
                    Name = "Cumin"
                },
                new Crop()
                {
                    Id = new Guid("b2ac2748-938d-4bfe-bbea-e48395ade6d9"),
                    Name = "Drumstick (Moringa)"
                },
                new Crop()
                {
                    Id = new Guid("4570d8b1-af2d-4efb-a6d5-62be71f31414"),
                    Name = "Dry Ginger"
                },
                new Crop()
                {
                    Id = new Guid("c0a6e077-d643-41a0-a5a9-d04048f8f41d"),
                    Name = "Dragon Fruit"
                },
                new Crop()
                {
                    Id = new Guid("96bad0ba-1d82-4137-845c-cc6fad0c2b00"),
                    Name = "Elephant Foot Yam"
                },
                new Crop()
                {
                    Id = new Guid("39119839-14fb-43a5-9ef9-837efc7e3425"),
                    Name = "Eggplant (Brinjal)"
                },
                new Crop()
                {
                    Id = new Guid("6732f622-ee4a-4b8d-9173-18f15b910727"),
                    Name = "Fennel"
                },
                new Crop()
                {
                    Id = new Guid("3c041b6a-84cf-4341-9684-5a2b3895ba90"),
                    Name = "Fenugreek"
                },
                new Crop()
                {
                    Id = new Guid("541e4e34-823e-40b2-b087-a665bf4e77a5"),
                    Name = "Fig"
                },
                new Crop()
                {
                    Id = new Guid("19eaf901-e9e9-4cd8-b15d-0e32fecc468f"),
                    Name = "Finger Millet (Ragi)"
                },
                new Crop()
                {
                    Id = new Guid("7595a84e-8d55-4511-9ec8-9fa64a941ad5"),
                    Name = "Garlic"
                },
                new Crop()
                {
                    Id = new Guid("bbcdf084-4be0-42ce-bba3-ed116ce97d67"),
                    Name = "Ginger"
                },
                new Crop()
                {
                    Id = new Guid("0ddc5c6b-ff47-462e-97ac-cbab2c9af3fb"),
                    Name = "Gladiolus"
                },
                new Crop()
                {
                    Id = new Guid("9b3fd166-7d79-4f3d-90ad-a019dbb69e4a"),
                    Name = "Gram (Chickpea)"
                },
                new Crop()
                {
                    Id = new Guid("e8a1efbe-52a7-484f-8662-0655f867f612"),
                    Name = "Grapes"
                },
                new Crop()
                {
                    Id = new Guid("ed09d3a0-a039-44c6-a04d-36a58e8541fe"),
                    Name = "Green Gram (Moong Dal)"
                },
                new Crop()
                {
                    Id = new Guid("5a361937-d993-4a29-b408-ce6ba3b3d06f"),
                    Name = "Guava"
                },
                new Crop()
                {
                    Id = new Guid("eab88297-32cc-408e-9a32-d97711cc3c5d"),
                    Name = "Horse Gram"
                },
                new Crop()
                {
                    Id = new Guid("eef6c972-c555-427f-bb0b-366db774624c"),
                    Name = "Hops"
                },
                new Crop()
                {
                    Id = new Guid("d35fd604-3dc1-4bef-b761-6b9d316c827f"),
                    Name = "Indian Gooseberry (Amla)"
                },
                new Crop()
                {
                    Id = new Guid("f3c09fc6-3ca3-4872-bb75-5a68b539c2ea"),
                    Name = "Ivy Gourd (Tindora)"
                },
                new Crop()
                {
                    Id = new Guid("01d02b48-e901-4e51-aa11-96997852a5b6"),
                    Name = "Jackfruit"
                },
                new Crop()
                {
                    Id = new Guid("80c5a3a0-9b23-4dbe-94ab-0866eb697aa3"),
                    Name = "Jowar (Sorghum)"
                },
                new Crop()
                {
                    Id = new Guid("ffa0793e-60df-45c7-a9bd-8e593446c129"),
                    Name = "Jute"
                },
                new Crop()
                {
                    Id = new Guid("bc61ab23-9b6b-4468-8e68-ce888424956b"),
                    Name = "Karonda (Bengal Currant)"
                },
                new Crop()
                {
                    Id = new Guid("9f304216-d257-4cc1-ad26-737b4408bafa"),
                    Name = "Kidney Beans (Rajma)"
                },
                new Crop()
                {
                    Id = new Guid("f6bfe5e1-db44-47c7-bf49-b90df4e52bfe"),
                    Name = "Kiwi"
                },
                new Crop()
                {
                    Id = new Guid("51e35670-f701-4d50-afe4-fe0b2a841268"),
                    Name = "Kohlrabi"
                },
                new Crop()
                {
                    Id = new Guid("8877c546-a49e-4198-a98b-bcb9cd8e47a4"),
                    Name = "Lady’s Finger (Okra)"
                },
                new Crop()
                {
                    Id = new Guid("ab205951-3a6a-4923-b89d-eaac3714a54a"),
                    Name = "Lemon"
                },
                new Crop()
                {
                    Id = new Guid("2b335a23-bedb-4a87-a913-78e123746a9b"),
                    Name = "Lettuce"
                },
                new Crop()
                {
                    Id = new Guid("9dfbb313-2e06-4c68-93ef-9e50130f411f"),
                    Name = "Linseed"
                },
                new Crop()
                {
                    Id = new Guid("8e8e3dbc-1f16-4cc8-b28a-a0bf6f21c828"),
                    Name = "Litchi"
                },
                new Crop()
                {
                    Id = new Guid("b76e55c7-d54c-425a-9ac0-92b134f965c3"),
                    Name = "Maize (Corn)"
                },
                new Crop()
                {
                    Id = new Guid("f9f5e7e0-41c9-4bbb-b477-78b1558dbe58"),
                    Name = "Mango"
                },
                new Crop()
                {
                    Id = new Guid("4269a493-312a-43b8-8eec-60a20d38b567"),
                    Name = "Millet (Pearl Millet - Bajra, Finger Millet - Ragi, Foxtail Millet, etc.)"
                },
                new Crop()
                {
                    Id = new Guid("d0590869-4f69-40b2-b8b0-18563506f239"),
                    Name = "Mint"
                },
                new Crop()
                {
                    Id = new Guid("57701715-632b-4f1a-afe8-188df01a4859"),
                    Name = "Mustard"
                },
                new Crop()
                {
                    Id = new Guid("4992c1ef-4c69-4772-ac3d-71807db61661"),
                    Name = "Nagpur Orange"
                },
                new Crop()
                {
                    Id = new Guid("dfac9837-c395-466d-9c22-734654a261ca"),
                    Name = "Nutmeg"
                },
                new Crop()
                {
                    Id = new Guid("325fc2d8-bab2-4a98-ab11-9c129230b092"),
                    Name = "Oats"
                },
                new Crop()
                {
                    Id = new Guid("93f6d429-a989-4cea-89ca-9997af000600"),
                    Name = "Onion"
                },
                new Crop()
                {
                    Id = new Guid("e7ce98ad-b589-4865-a0dd-b3d5c03c838d"),
                    Name = "Orange"
                },
                new Crop()
                {
                    Id = new Guid("79d75629-5079-4bb8-aa47-97ec6aa89cff"),
                    Name = "Papaya"
                },
                new Crop()
                {
                    Id = new Guid("07af231b-dc20-4b84-ae5b-dbf7b8b371cc"),
                    Name = "Passion Fruit"
                },
                new Crop()
                {
                    Id = new Guid("a30b60ae-3912-48c8-8b14-1d1a1c6195c4"),
                    Name = "Peach"
                },
                new Crop()
                {
                    Id = new Guid("b1872588-b840-4e1a-8032-1f54245e0eff"),
                    Name = "Pear"
                },
                new Crop()
                {
                    Id = new Guid("17444e41-e408-42b2-b83e-7587549c1936"),
                    Name = "Peas"
                },
                new Crop()
                {
                    Id = new Guid("0f24ec89-6c5e-4af3-84a8-412f8c836532"),
                    Name = "Pepper (Black Pepper)"
                },
                new Crop()
                {
                    Id = new Guid("a3145ade-9b52-42cd-8b5c-87d4ac519d29"),
                    Name = "Pigeon Pea (Tur Dal)"
                },
                new Crop()
                {
                    Id = new Guid("0835d5f2-adaf-4339-ae59-ad6580cc6f04"),
                    Name = "Pineapple"
                },
                new Crop()
                {
                    Id = new Guid("688c3609-ad70-4e64-978b-9ce870dd47b3"),
                    Name = "Plum"
                },
                new Crop()
                {
                    Id = new Guid("6ea61254-e33f-4f98-84ec-f7558b603888"),
                    Name = "Pomegranate"
                },
                new Crop()
                {
                    Id = new Guid("18924fea-41c6-4e33-b77a-ab97bdac56f1"),
                    Name = "Potato"
                },
                new Crop()
                {
                    Id = new Guid("81b63ebb-f304-409f-a03a-313ca8c471c0"),
                    Name = "Pumpkin"
                },
                new Crop()
                {
                    Id = new Guid("a7e844ca-00b1-4d53-8198-583f0ade31b6"),
                    Name = "Poppy Seed"
                },
                new Crop()
                {
                    Id = new Guid("675e5a94-a63f-4dc5-b678-2b8a35b87bd2"),
                    Name = "Radish"
                },
                new Crop()
                {
                    Id = new Guid("e4476b7d-c642-44a4-b81f-ffc1c7b76f65"),
                    Name = "Raisin"
                },
                new Crop()
                {
                    Id = new Guid("fa4d7601-158b-4f59-8ae4-5554ab6ee059"),
                    Name = "Ramtil (Niger Seed)"
                },
                new Crop()
                {
                    Id = new Guid("e4cd0b00-eb7b-481f-8e25-540e29f5912a"),
                    Name = "Red Gram (Arhar/Tur Dal)"
                },
                new Crop()
                {
                    Id = new Guid("2d63ebbf-b353-4931-ad29-009c84b69069"),
                    Name = "Rice"
                },
                new Crop()
                {
                    Id = new Guid("c2dc66df-23cf-46e7-b467-06494e5aa91d"),
                    Name = "Safflower"
                },
                new Crop()
                {
                    Id = new Guid("ed4d2cb3-c732-4d0f-95e3-cd3152b6038b"),
                    Name = "Saffron"
                },
                new Crop()
                {
                    Id = new Guid("e3aeb357-8928-4400-b139-18fb19500bb1"),
                    Name = "Sesame (Til)"
                },
                new Crop()
                {
                    Id = new Guid("6a1da064-18c0-4aea-b0f5-4b88bc055b1f"),
                    Name = "Soybean"
                },
                new Crop()
                {
                    Id = new Guid("b6f08953-764d-4734-8ae7-c27fb5b44ada"),
                    Name = "Spinach"
                },
                new Crop()
                {
                    Id = new Guid("4228dfb0-834d-4577-8ccd-8ad65dfcddaa"),
                    Name = "Strawberry"
                },
                new Crop()
                {
                    Id = new Guid("b7d03a84-d48f-4e5b-aaef-8c21bfc94a3d"),
                    Name = "Sugarcane"
                },
                new Crop()
                {
                    Id = new Guid("f00484d0-344a-4c76-970b-4a2f55dfd652"),
                    Name = "Sunflower"
                },
                new Crop()
                {
                    Id = new Guid("7d08f4a8-5f0e-404e-a629-811295f9071c"),
                    Name = "Sweet Corn"
                },
                new Crop()
                {
                    Id = new Guid("0ef3f90c-e2c3-4b9f-bde1-6e745cf1c636"),
                    Name = "Sweet Potato"
                },
                new Crop()
                {
                    Id = new Guid("de496553-4a9f-4f49-8d52-4ace762c0b2b"),
                    Name = "Tamarind"
                },
                new Crop()
                {
                    Id = new Guid("650527dc-93ad-4cd9-9889-1e9c152420d6"),
                    Name = "Tapioca"
                },
                new Crop()
                {
                    Id = new Guid("0e58d5fb-460e-4110-a8a7-2ca385970453"),
                    Name = "Tea"
                },
                new Crop()
                {
                    Id = new Guid("2dba4472-6cfc-414e-b1ae-6ab114080b2a"),
                    Name = "Tomato"
                },
                new Crop()
                {
                    Id = new Guid("34ca5fc6-56af-4565-96d9-82a45a4b22e7"),
                    Name = "Turmeric"
                },
                new Crop()
                {
                    Id = new Guid("aca04646-c590-4443-9aaa-7c5882611e42"),
                    Name = "Vanilla"
                },
                new Crop()
                {
                    Id = new Guid("dcd31b07-d65d-4a9c-b86b-3f0c8f0517fa"),
                    Name = "Walnut"
                },
                new Crop()
                {
                    Id = new Guid("d3384bf8-fd25-4fdc-92c5-2e9d7c3474ae"),
                    Name = "Watermelon"
                },
                new Crop()
                {
                    Id = new Guid("45cc4d78-6c19-41df-9aa3-6f6d218dc84a"),
                    Name = "Wheat"
                },
                new Crop()
                {
                    Id = new Guid("8576b0b0-1085-42a6-9e50-22922c3b35d0"),
                    Name = "Yam"
                }
            );
            _context.SaveChanges();
        }

        if (!_context.SoilTypes.Any())
        {
            _context.SoilTypes.AddRange(
                new SoilType()
                {
                    Id = new Guid("86b7bb6e-4cd6-4ced-bb53-986860e00bb8"),
                    Name = "Sandy"
                },
                new SoilType()
                {
                    Id = new Guid("8c89dca0-1f9b-4133-ba91-0f1a152e764e"),
                    Name = "Slit"
                },
                new SoilType()
                {
                    Id = new Guid("a7f452d2-8773-4623-8d13-94a3de73b3c7"),
                    Name = "Clay"
                },
                new SoilType()
                {
                    Id = new Guid("3f7a2c9b-5b4e-4a8d-b4a0-1e6f6f7c3b2e"),
                    Name = "Lumpy"
                },
                new SoilType()
                {
                    Id = new Guid("e1d8c2fa-9b5e-4e1b-8237-8c5d8dbe4d71"),
                    Name = "Peaty"
                },
                new SoilType()
                {
                    Id = new Guid("b29f9ea1-49c2-44f7-91f2-6d1e1ebd88a7"),
                    Name = "Saline"
                },
                new SoilType()
                {
                    Id = new Guid("d7f8a62b-18bb-41f6-a9b2-4f9c96e2efb5"),
                    Name = "Chalky"
                },
                new SoilType()
                {
                    Id = new Guid("a4e6cb83-4cf0-403a-bc86-1fef2ac597b7"),
                    Name = "Black"
                },
                new SoilType()
                {
                    Id = new Guid("9c5ad1fa-1d3f-42c0-8898-b23e1a84e8fb"),
                    Name = "Red"
                }
            );

            _context.SaveChanges();
        }

        if (!_context.CropStages.Any())
        {
            _context.CropStages.AddRange(
                new CropStage()
                {
                    Id = new Guid("c1f2b3e4-5d6a-7f8b-9c0d-1e2f3a4b5c6d"),
                    Name = "Sowing germination"
                },
                new CropStage()
                {
                    Id = new Guid("a9b8c7d6-e5f4-3a2b-1c0d-9e8f7b6a5d4c"),
                    Name = "Seeding and vegetative growth"
                },
                new CropStage()
                {
                    Id = new Guid("f1e2d3c4-b5a6-7980-1c2d-3e4f5b6a7d8c"),
                    Name = "Flowing"
                },
                new CropStage()
                {
                    Id = new Guid("8b7a6d5c-4f3e-2b1a-0c9d-8f7e6d5c4b3a"),
                    Name = "Fruiting and maturation"
                },
                new CropStage()
                {
                    Id = new Guid("5c4b3a2f-1e0d-9c8b-7a6d-5f4e3d2c1b0a"),
                    Name = "Harvesting and past harvesting"
                }
            );

            _context.SaveChanges();
        }
    }
}
