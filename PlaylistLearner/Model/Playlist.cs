using Jalindi.VideoUtil.Util;

namespace PlaylistLearner.Model;

public enum OrderBy { Sequence, OrderNumber, StartNumber, EndNumber}

public class Playlist
{
    public bool Silent { get; init; }
    public bool SpeedControls { get; init; }
    public OrderBy OrderBy { get; init; }
    
    public string Name { get; init; }
    public string Description { get; init; }
    public string? Link { get; init; }
    public string? LinkText { get; init; }
    public List<PlaylistItem> Items { get; init;} = new();


    public static Playlist Academia2005 = new Playlist()
    {
        Name = "Academia de Salsa 2005", Description = "",
        Link = "https://www.academiadesalsa.com/", LinkText = "Academia de Salsa",
        Items =
        {
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Intro", "","",
                "", "https://youtu.be/PppjDo0kLg8", 849, 875),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Salsa Music Band", "","",
                "", "https://youtu.be/PppjDo0kLg8", 1445, 2504),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Basic Mambo", "","",
                "", "https://youtu.be/ue-P8DoJ29Q", 6, 13),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Basic Mambo Side to Side", "","",
            "", "https://youtu.be/ue-P8DoJ29Q", 13, 21),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Open and Close", "","",
                "", "https://youtu.be/ue-P8DoJ29Q", 21, 31),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Dile que no", "","Cross body lead",
                "", "https://youtu.be/ue-P8DoJ29Q", 31, 36),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Guapea", "","",
                "", "https://youtu.be/ue-P8DoJ29Q", 36, 40),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Enchufe", "","Plug in",
                "", "https://youtu.be/ue-P8DoJ29Q", 40, 48),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Enchufe doble", "","Plug in double",
                "", "https://youtu.be/ue-P8DoJ29Q", 48, 62),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Enchufe por detras", "","Plug in from behind",
                "", "https://youtu.be/ue-P8DoJ29Q", 62, 68),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Sacala", "","Take her out",
                "Show follower off with right hand", "https://youtu.be/ue-P8DoJ29Q", 68, 76),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Sombrero", "","Hat",
                "", "https://youtu.be/ue-P8DoJ29Q", 76, 87),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Sombrero con mambo", "","Hat with mambo",
                "", "https://youtu.be/ue-P8DoJ29Q", 87, 101),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Ocho", "","Eight",
                "", "https://youtu.be/ue-P8DoJ29Q", 101, 117),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Vacilala", "","Tease her",
                "", "https://youtu.be/ue-P8DoJ29Q", 117, 133),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Dedo", "","Finger",
                "", "https://youtu.be/ue-P8DoJ29Q", 133, 137),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Up close and personal", "","",
                "", "https://youtu.be/ue-P8DoJ29Q", 137, 150),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Abanico Complicado con Adorno", "","Complicated fan with adornment",
            "", "https://youtu.be/CPNBTjto-Jc", 0, 41),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Bayamo con Echeverria", "","",
                    "", "https://youtu.be/CPNBTjto-Jc", 41, 68),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Caminala & Guapea con punta delpie", "","Caminala & Guapea with toe-tip",
                    "", "https://youtu.be/CPNBTjto-Jc", 68, 95),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Enrosca y Desenrosca", "","Twist & Unscrew",
                    "", "https://youtu.be/CPNBTjto-Jc", 95, 120),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Paseala por Detras, Arriba y a lo moderno", "","Back, Up and Modern",
                    "", "https://youtu.be/CPNBTjto-Jc", 120, 160),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Paseala con Adorno", "","Walk her with adornment",
                    "", "https://youtu.be/CPNBTjto-Jc", 160, 237),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Setenta con Adorno y Echeverria", "","Seventy with adornment & Echeverria",
                    "", "https://youtu.be/CPNBTjto-Jc", 237, 279),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Son & Turning on the spot", "","",
                    "", "https://youtu.be/CPNBTjto-Jc", 279, 345),
            new PlaylistItem(ItemType.Default, AspectRatio.FourThree, "Credits", "","",
            "", "https://youtu.be/PppjDo0kLg8", 2788, 2830),

        }
    };

    public static Playlist Salsa = new Playlist()
    {
        Name = "Salsa", Description = "",
        Items =
        {
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Side together side", "","",
                "Left Side Step Side Step, Right Side Step Side Step", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Baby back steps", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Open and close", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Half and half turn", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Forward and back", "","Basic", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Catwalk turn", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Half Turn", "","", "", "https://youtu.be/kJwnuHlT8lQ"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Exhíbela", "","Show her off", "Show her off with left hand", "https://youtu.be/--SPhRWYJcI"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Sacala", "","Take it out", "Show her off with right hand", "https://www.youtube.com/watch?v=pgd4jcPJHow"),
            
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Exhíbela ronde", "","", "", "https://youtu.be/fLf2sVVcO5Q"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Dile que no", "","Cross Body Lead", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Open Dile que no", "","Open Cross Body Lead",
                "Cross Body Lead right hand, left open", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Sombrero", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Enchúfela", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Cuddle Turn", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Reverso", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Corkscrew", "","", "", ""),

            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Catwalk turn, spin out and recall", "","",
                "Catwalk turn, Cuddle Turn, Spin Out, Recall", "https://youtu.be/aeeNexrnrZA "),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Catwalk turn, spin out and recall & Lean", "","",
                "Catwalk turn, Cuddle Turn, Spin Out, Recall, Lean, Spin out, Recall", "https://youtu.be/XDhNxe08EQg"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Dizzy dedo", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Exhíbela Zero", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Enchúfela doble y quédate", "","",
                "Enchúfela then stop the follower 456 and the Enchúfela", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Enchúfela doble y quédate", "","", "Enchúfela, Enchúfela, Ronde", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Enchúfela el Alarde", "","", "",
                "https://www.youtube.com/watch?v=krpmwN1nfXM"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Sombrero doble", "","",
                "Sombrero 123 456, sombrero to mans left side. Then single sombrero back to mans right side, dile que no",
                "https://www.youtube.com/watch?v=vX7zMTs8eJA"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Bayamo", "","",
                "Single hand sombrero. 456 around the man. 456 man turn anticlock-wise to woman right hand over head. Setenta to dile que no",
                "https://www.youtube.com/watch?v=lFaJ4W0LfCM"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Bayamo por abajo", "","Bayamo below",
                "Single hand sombrero. 456 around the man. 123 left hand arm lock 456 come out. 123456 sombrero and hat. Dile que no.",
                "https://www.youtube.com/watch?v=KO9y_5HyWKg"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Montana", "","Eighty-four",
                "Two hand no hat sombrero (123456). Anti clockwise 123 woman. 456 outside turn for man. Then sombrero y dile que no",
                ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Ochenta y quatro", "","Eighty-four",
                "Two hand no hat sombrero (123456). Anti clockwise 123 woman. 456 outside turn for man. Then quédate to draw follower",
                "https://www.youtube.com/watch?v=GYzMYLR6NHg"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "El uno", "","The one", "", "https://www.youtube.com/watch?v=FZAvKSU2I7E"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "La complete", "","", "The complete set of Enchúfelas", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Puerto", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Club taco", "","", "Sombrero start, catch elbow 123 - hat 567, prep 1 back over head 23, ronde 567, prep 123 two hand spin left 567, prep 123 two hand spin right hat 567, dile que no",
                "https://www.youtube.com/watch?v=W_kXoJvl-qA"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Coca cola", "","", "Shape like the coca cola bottle",
                "https://youtu.be/Y_iN6x5z7Uc"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Ochos", "","Eight", "", "https://youtu.be/wIZc1URrHMo"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Ochos: left handed and Enchúfela", "","", "",
                "https://youtu.be/suSqLQbBRPs"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Ochos: left handed and complicado", "","", "",
                "https://youtu.be/_YIC9pwdKs8"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Kentucky", "","",
                "Cuddle, then cuddle to neck, then over head man turn clockwise to finish with dile que no",
                "https://www.youtube.com/watch?v=sK1SlAIE0LA"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Setenta", "","Seventy", "", "https://youtu.be/5PB_sh_qu7A"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Setenta Tresario", "","Seventy-three", "", "https://youtu.be/2Kk43iStQXQ"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Arco iris", "","Rainbow",
                "Hammerlock, Enchúfela, Circle the left, Right across face, back to back, right / left, right / left, Stretch (follower turns right), alarde (left hand reverse), Enchúfela, alarde (right hand) - Setenta (123456), 123 unwind woman, 456 man in sweet heart position. 123 la Cruz. 456 Mans left hand up turns clockwise and woman clockwise.",
                "https://youtu.be/qXw-impfp5k"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "4 corners?", "","", "", ""),

            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Guapea", "","", "", "https://www.youtube.com/watch?v=YwxB1MSytYA"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Crisscross", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Push and throw", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Photo", "","", "Pose for a photo", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Media", "","", "Middle clap on 1", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Short cut sombrero con mambo", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Tumba francesa", "","",
                "Right hand, Enchúfela, right hand, left hand, right hand, left hand behind back, Enchúfela damit", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Tira la sabana", "","", "", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Dame", "","", "Pass partner on", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Dame Duo", "","", "Pass partner on, skip", ""),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Week 7", "","", "", "https://youtu.be/dljQBg9QTL8"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Week 6", "","", "", "https://youtu.be/eSzx9SdieZg"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Coffee Shop", "","", "", "https://youtu.be/NH2rVg3eaU4"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Funny ", "","", "", "https://www.youtube.com/watch?v=XzsFMy8yQ48"),
            new PlaylistItem(ItemType.Default, AspectRatio.SixteenNine, "Cuddle Turn variation", "","", "",
                "https://www.youtube.com/watch?v=R8aZMRdMa6g"),
            new PlaylistItem(ItemType.HyperLink, AspectRatio.SixteenNine, "Rueda calls", "","", "", "http://www.salsaclass.co.uk/rueda_calls.htm"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Bachata Combo - Cha Cha Block", "","", "", "https://youtu.be/yzvTGFg80Hc"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Bachata Combo - Pretzel, Wave ", "","", "",
                "https://youtu.be/HwZKuDGsn_M"),
            new PlaylistItem(ItemType.Default, AspectRatio.NineSixteen, "Bachata Combo - The Matrix", "","", "", "https://youtu.be/TXkVrX3TRsw"),

        }
    };
}