/**
 * Mark Sarasua
 * Dr. Alrifai
 * CS 4253
 * Final Exam Alternative Project
 * ACMFinalExamProblem_SQL.sql
 * 
 */

CREATE DATABASE CrossWordPuzzles;
GO

USE CrossWordPuzzles;
GO

CREATE TABLE Puzzles
(
	[Puzzle Number] INT IDENTITY(1,1) PRIMARY KEY,
	[Row Count] INT NOT NULL,
	[Column Count] INT NOT NULL,
	[Word Count] INT NOT NULL,
	[Letters] TEXT NOT NULL,
	[Words] TEXT NOT NULL
);
GO

INSERT INTO Puzzles ([Row Count],[Column Count],[Word Count],[Letters],[Words])
VALUES
(
	10,20,10,
	'AFSTHCSETTINGUWDYZPGVPUZPZICPHMWSABXTAPIOBWECHBMRGYZZTJIVVSTIROQHFECOIRRCICCIXNNDUQGVKMRCNTIUOGIAFSEUAQLGPFVEDNFFERUTXIMTXINJURYDIUJPDRBAUYYSPMRMBESUMODDZCXDGAAUJESUUQSRTCQECATUVBPSQMUSINGERCPDEWDFWQW',
	'INJURY,MIDNIGHT,MIXTURE,DATA,SETTING,STUDIO,PROCEDURE,PAYMENT,COUNTRY,SINGER'
),
(
	11,12,10,
	'FJUTWPJRFBJILLIVEOADWMXOHKDAICNIWZYGYSUHVJTAHHQFUYLNOUNMCWGAFRHOMEWORKNIJOSREVENUEWVYTEFASFDXUTDISECRETARYNAUIMROFTALPSSZHRGKWRITERJ',
	'MOVIE,SAFETY,HISTORY,HAT,DIAMOND,SECRETARY,PLATFORM,WRITER,REVENUE,HOMEWORK'
),
(
	11,16,10,
	'WHTMQXIVMOORHTABLBECGKNHASYKTPMRJZDGNFSRREDEZVBVNVUQIKUBRBQWQMIKSOCQDCRAIHFNNCVWCGAUNBASADYRTNEEXXTAIHNKGNREEBSVWHILFQCEEWXLZJGMCBOITAETWQTPANZUTXNTDYABEMPLOYEEOOFYVJXCXQZPECWW',
	'MARRIAGE,BEER,BASKET,QUALITY,INSURANCE,ENTRY,EDUCATION,BATHROOM,FINDING,EMPLOYEE'
),
(
	10,16,10,
	'UKFUDAKGANALYSISAESTNSHJPWOOFMREFDRKZHCFWPTUPEOKMUZEOWROTRXFDTTAJTKGACUOCOEDYSCLIIPERDHTUPPPQYEOZTSJOCCBQEUWASRVSTDHIDEAWRFUPRIELACHOCOLATEJIUDNAQBDFLOLHYKRBEIF',
	'ANALYSIS,CHURCH,CHOCOLATE,IDEA,SYSTEM,PROPERTY,DIRECTOR,LAKE,ATTITUDE,FOOTBALL'
),
(
	12,20,10,
	'LGFBHXYNSBEIWSYMKDDEPJJYRPDLGRIRSUVAPVCXHZRUMPFTGVIRUSSJPHZHRJCOPOTATOEMOCNIRBOTNRLHLMBKVRCXHEDXBXHLMFBCAPXDANKXGWLAZRPAQQCABINETJIIGCXEBJCEDEYWBVARBPSWRITERIIHDALCOHOLTLIMITOYQEDYTANLGJGDUTTDVYOSBNDNFWAOEIQLYVKSCOADSDRGVOQNHYROEHTHKBBNSVWI',
	'THEORY,CITY,HEALTH,POTATO,ALCOHOL,VIRUS,WRITER,INCOME,LAB,CABINET'
),
(
	10,12,10,
	'HXNIAREFKLFCBHBRIMQOJQBZDROFFAREUGRAETNETUBIRTTACSPEAKLSJCFSRBCTEIIHXSPXEDVIFQFSVHKPAWTMEPQLEWHRSDXEDIRGQZPCEPILSIHNHDIE',
	'SPEAK,ARGUE,TIME,ATTRIBUTE,BRIM,RAIN,AFFORD,SLIP,DEFEAT,DECREASE'
),
(
	11,18,10,
	'QADCBBMVQIELASTICZCVASTJYTNELLECXEDKOZSHWKFRTFDRLSRNLOCBGIFLDAIMXEEHSKPVVWSNXAGIFTEDTGXYQRDUQYWTCAKPXQLVLHNHYFJOSUOICILAMOVFIHNIDDPTSSELDNUOBFZCWONDERFULKINDLYKLLKOVRNRBAXFZVYWOXHTGIARHFJNNMSEYKAHSE',
	'ODD,GIFTED,ELASTIC,KINDLY,WONDERFUL,BOUNDLESS,MALICIOUS,SHINY,EXCELLENT,SHAKY'
),
(
	12,19,10,
	'RNRMFYJJZLITTLEAYJHEABXDXXZMKNQILCQQXKWXRIAWMXSJNZIPMOCNHZRMUGZZICGPOXHEJVYLISXHAPPYHIZGPBLYMBDGOPJIVSZCAQYOOLDEXBJERBNPVNDNNJGZOEBXIFMAWFORGETFULWWMCZRTJHWUXRYODHLHVBRMLIDSSELPAHVSPBUBKAZFEZRYZFZCYETAVIRPNSCHMBZNGNTEAPXDPAOUBAI',
	'LITTLE,GAINFUL,UNARMED,FORGETFUL,PRIVATE,HAPPY,SHARP,MELLOW,HAPLESS,GIANT'
),
(
	11,13,10,
	'BUGLLCHVXTANLFMNTZXNIMYZDAGOIGIWSYGBHAZDLONQUICKBDMYEDGIDCCHUUCVETYTPYZZFWHSPPARUMNAIRMCHCOCUOIAOIYCIMZLUUQLMJUKCZTNEDPKGIMKYPPEWPEEYNCGBSNCLMK',
	'ICY,LAZY,CHUBBY,TAN,QUICK,EDUCATED,DYNAMIC,LIMPING,MOLDY,OUTGOING'
),
(
	12,12,10,
	'BTCPKGNVKEUKDZXTYETEARJKNVETALUCEPSUQZPNDBYGWBNSMBOJYAETRHHLNRJINTSWNSOUIEBIAEAHOAPFFOORMGCTMMTQQXKZIETKMSIABGCOCVISOUOSNIIMIWUCCENQFIRSTESFXMWO',
	'SPECULATE,VEGETABLE,ROOF,COMMON,SMASH,DYNAMIC,WEAK,SUITCASE,OPTION,FIRST'
);
GO