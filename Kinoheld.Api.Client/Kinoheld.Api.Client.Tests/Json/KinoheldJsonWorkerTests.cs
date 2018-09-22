﻿using System;
using System.Linq;
using Kinoheld.Api.Client.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Kinoheld.Api.Client.Tests.Json
{
    [TestFixture(Category = "L0")]
    public class KinoheldJsonWorkerTests
    {
        [Test]
        public void ConvertToCinemas_DoesNotAllowNullJson()
        {
            IKinoheldJsonWorker worker = new KinoheldJsonWorker();
            Assert.Throws<ArgumentNullException>(() => worker.ConvertToCinemas(null));
        }

        [Test]
        public void ConvertToShows_DoesNotAllowNullJson()
        {
            IKinoheldJsonWorker worker = new KinoheldJsonWorker();
            Assert.Throws<ArgumentNullException>(() => worker.ConvertToShows(null));
        }

        [Test]
        public void ConvertToCinemas_ConvertsTwoCinemasSucesssfully()
        {
            IKinoheldJsonWorker worker = new KinoheldJsonWorker();
            var cinemas = worker.ConvertToCinemas(JObject.Parse(
                "{\r\n  \"cinemas\": [\r\n    {\r\n      \"id\": \"2127\",\r\n      \"name\": \"Kino Aurich\",\r\n      \"street\": \"Emder Straße 5\",\r\n      \"city\": {\r\n        \"name\": \"Aurich\"\r\n      },\r\n      \"distance\": 3.2250148700598,\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich\"\r\n      }\r\n    },\r\n    {\r\n      \"id\": \"1613\",\r\n      \"name\": \"Autokino Aurich-Tannenhausen\",\r\n      \"street\": \"Am Stadion 14\",\r\n      \"city\": {\r\n        \"name\": \"Aurich\"\r\n      },\r\n      \"distance\": 4.7613563356582,\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/autokino-aurich-tannenhausen?layout=shows\"\r\n      }\r\n    }\r\n  ]\r\n}"));

            Assert.AreEqual(2, cinemas.Count());
            Assert.AreEqual("Kino Aurich", cinemas.First().Name);
            Assert.AreEqual("Aurich", cinemas.First().City.Name);
            Assert.AreEqual("2127", cinemas.First().Id);
            Assert.AreEqual("Emder Straße 5", cinemas.First().Street);
            Assert.AreEqual("Autokino Aurich-Tannenhausen", cinemas.Last().Name);
            Assert.AreEqual("Aurich", cinemas.Last().City.Name);
            Assert.AreEqual("1613", cinemas.Last().Id);
            Assert.AreEqual("Am Stadion 14", cinemas.Last().Street);
        }

        [Test]
        public void ConvertToCinemas_ThrowsExceptionWhenBadFormat()
        {
            IKinoheldJsonWorker worker = new KinoheldJsonWorker();
            Assert.Throws<InvalidCastException>(() => worker.ConvertToCinemas(JObject.Parse("{\r\n  \"cinemass\": [\r\n    {\r\n      \"id\": \"2127\",\r\n      \"name\": \"Kino Aurich\",\r\n      \"street\": \"Emder Straße 5\",\r\n      \"city\": {\r\n        \"name\": \"Aurich\"\r\n      },\r\n      \"distance\": 3.2250148700598,\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich\"\r\n      }\r\n    },\r\n    {\r\n      \"id\": \"1613\",\r\n      \"name\": \"Autokino Aurich-Tannenhausen\",\r\n      \"street\": \"Am Stadion 14\",\r\n      \"city\": {\r\n        \"name\": \"Aurich\"\r\n      },\r\n      \"distance\": 4.7613563356582,\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/autokino-aurich-tannenhausen?layout=shows\"\r\n      }\r\n    }\r\n  ]\r\n}")));
        }


        [Test]
        public void ConvertToShows_ConvertsShowsSucesssfully()
        {
            IKinoheldJsonWorker worker = new KinoheldJsonWorker();
            var shows = worker.ConvertToShows(JObject.Parse(
                "{\r\n  \"shows\": [\r\n    {\r\n      \"name\": \"Haus der geheimnisvollen Uhren, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275006/haus-der-geheimnisvollen-uhren-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Fantasy\"\r\n          },\r\n          {\r\n            \"name\": \"Sci-Fi\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Pettersson und Findus 3 - Findus zieht u\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274953/pettersson-und-findus-3-findus-zieht-u\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Klassentreffen 1.0 - Die unglaubliche Re\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274988/klassentreffen-1-0-die-unglaubliche-re\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Hotel Transsilvanien 3 - Ein Monster Url\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [\r\n        {\r\n          \"name\": \"3D\"\r\n        }\r\n      ],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275040/hotel-transsilvanien-3-ein-monster-url\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Animation\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Searching\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274973/searching\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Drama\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Schönste Mädchen der Welt, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275023/schoenste-maedchen-der-welt-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Schönste Mädchen der Welt, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275024/schoenste-maedchen-der-welt-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Klassentreffen 1.0 - Die unglaubliche Re\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274989/klassentreffen-1-0-die-unglaubliche-re\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Haus der geheimnisvollen Uhren, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275007/haus-der-geheimnisvollen-uhren-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Fantasy\"\r\n          },\r\n          {\r\n            \"name\": \"Sci-Fi\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Book Club - Das Beste kommt noch\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274962/book-club-das-beste-kommt-noch\"\r\n      },\r\n      \"movie\": {\r\n      \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Nun, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275049/nun-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Horrorfilm\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Searching\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274974/searching\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Drama\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Klassentreffen 1.0 - Die unglaubliche Re\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274990/klassentreffen-1-0-die-unglaubliche-re\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Predator - Upgrade\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [\r\n        {\r\n          \"name\": \"3D\"\r\n        }\r\n      ],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275034/predator-upgrade\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Action\"\r\n          },\r\n          {\r\n            \"name\": \"Sci-Fi\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Equalizer 2, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275020/equalizer-2-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Action\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Meg, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [\r\n        {\r\n          \"name\": \"3D\"\r\n        }\r\n      ],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274967/meg-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Science Fiction\"\r\n          },\r\n          {\r\n            \"name\": \"Action\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Nun, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275050/nun-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Horrorfilm\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Searching\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274975/searching\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Drama\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    }\r\n  ]\r\n}"));

            Assert.AreEqual(18, shows.Count());

            Assert.AreEqual("Haus der geheimnisvollen Uhren, Das", shows.First().Name);
            Assert.AreEqual("21.09.2018 16:30", shows.First().Beginning.Formatted);
            Assert.AreEqual("https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275006/haus-der-geheimnisvollen-uhren-das", shows.First().DetailUrl.AbsoluteUrl);
            Assert.AreEqual("Fantasy", shows.First().MovieInfo.Genres[0].Name);
            Assert.AreEqual("Searching", shows.Last().Name);
            Assert.AreEqual("21.09.2018 22:30", shows.Last().Beginning.Formatted);
            Assert.AreEqual("https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274975/searching", shows.Last().DetailUrl.AbsoluteUrl);
            Assert.AreEqual("Drama", shows.Last().MovieInfo.Genres[0].Name);
        }

        [Test]
        public void ConvertToShows_ThrowsExceptionWhenBadFormat()
        {
            IKinoheldJsonWorker worker = new KinoheldJsonWorker();
            Assert.Throws<InvalidCastException>(() => worker.ConvertToShows(JObject.Parse(
                "{\r\n  \"showss\": [\r\n    {\r\n      \"name\": \"Haus der geheimnisvollen Uhren, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275006/haus-der-geheimnisvollen-uhren-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Fantasy\"\r\n          },\r\n          {\r\n            \"name\": \"Sci-Fi\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Pettersson und Findus 3 - Findus zieht u\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274953/pettersson-und-findus-3-findus-zieht-u\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Klassentreffen 1.0 - Die unglaubliche Re\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274988/klassentreffen-1-0-die-unglaubliche-re\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Hotel Transsilvanien 3 - Ein Monster Url\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [\r\n        {\r\n          \"name\": \"3D\"\r\n        }\r\n      ],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275040/hotel-transsilvanien-3-ein-monster-url\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Animation\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Searching\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274973/searching\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Drama\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Schönste Mädchen der Welt, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 16:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275023/schoenste-maedchen-der-welt-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Schönste Mädchen der Welt, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275024/schoenste-maedchen-der-welt-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Klassentreffen 1.0 - Die unglaubliche Re\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274989/klassentreffen-1-0-die-unglaubliche-re\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Haus der geheimnisvollen Uhren, Das\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275007/haus-der-geheimnisvollen-uhren-das\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Fantasy\"\r\n          },\r\n          {\r\n            \"name\": \"Sci-Fi\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Book Club - Das Beste kommt noch\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274962/book-club-das-beste-kommt-noch\"\r\n      },\r\n      \"movie\": {\r\n      \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Nun, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275049/nun-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Horrorfilm\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Searching\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 19:45\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274974/searching\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Drama\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Klassentreffen 1.0 - Die unglaubliche Re\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274990/klassentreffen-1-0-die-unglaubliche-re\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Komödie\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Predator - Upgrade\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [\r\n        {\r\n          \"name\": \"3D\"\r\n        }\r\n      ],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275034/predator-upgrade\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Action\"\r\n          },\r\n          {\r\n            \"name\": \"Sci-Fi\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Equalizer 2, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275020/equalizer-2-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Action\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Meg, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [\r\n        {\r\n          \"name\": \"3D\"\r\n        }\r\n      ],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274967/meg-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Science Fiction\"\r\n          },\r\n          {\r\n            \"name\": \"Action\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Nun, The\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1275050/nun-the\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Horrorfilm\"\r\n          }\r\n        ]\r\n      }\r\n    },\r\n    {\r\n      \"name\": \"Searching\",\r\n      \"beginning\": {\r\n        \"formatted\": \"21.09.2018 22:30\"\r\n      },\r\n      \"flags\": [],\r\n      \"detailUrl\": {\r\n        \"absoluteUrl\": \"https://www.kinoheld.de/kino-aurich/kino-aurich/vorstellung/1274975/searching\"\r\n      },\r\n      \"movie\": {\r\n        \"genres\": [\r\n          {\r\n            \"name\": \"Drama\"\r\n          },\r\n          {\r\n            \"name\": \"Thriller\"\r\n          }\r\n        ]\r\n      }\r\n    }\r\n  ]\r\n}")));
        }
    }
}