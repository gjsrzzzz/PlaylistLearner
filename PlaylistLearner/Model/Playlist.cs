﻿namespace PlaylistLearner.Model;

public class Playlist
{
    public string Name { get; init; }
    public List<PlaylistItem> Items { get; } = new();

    public static Playlist Salsa = new Playlist()
    {
        Name = "Salsa",
        Items =
        {
            new PlaylistItem(ItemType.Default, Format.Default, "Side together side", "",
                "Left Side Step Side Step, Right Side Step Side Step", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Baby back steps", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Open and close", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Half and half turn", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Forward and back", "Basic", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Catwalk turn", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Half Turn", "", "", "https://youtu.be/kJwnuHlT8lQ"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Exhíbela", "Show her off", "Show her off with left hand", "https://youtu.be/--SPhRWYJcI"),
            new PlaylistItem(ItemType.Default, Format.Default, "Sacala", "Take it out", "Show her off with right hand", "https://www.youtube.com/watch?v=pgd4jcPJHow"),
            
            new PlaylistItem(ItemType.Default, Format.Mobile, "Exhíbela ronde", "", "", "https://youtu.be/fLf2sVVcO5Q"),
            new PlaylistItem(ItemType.Default, Format.Default, "Dile que no", "Cross Body Lead", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Open Dile que no", "Open Cross Body Lead",
                "Cross Body Lead right hand, left open", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Sombrero", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Enchúfela", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Cuddle Turn", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Reverso", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Corkscrew", "", "", ""),

            new PlaylistItem(ItemType.Default, Format.Mobile, "Catwalk turn, spin out and recall", "",
                "Catwalk turn, Cuddle Turn, Spin Out, Recall", "https://youtu.be/aeeNexrnrZA "),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Catwalk turn, spin out and recall & Lean", "",
                "Catwalk turn, Cuddle Turn, Spin Out, Recall, Lean, Spin out, Recall", "https://youtu.be/XDhNxe08EQg"),
            new PlaylistItem(ItemType.Default, Format.Default, "Dizzy dedo", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Exhíbela Zero", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Enchúfela doble y quédate", "",
                "Enchúfela then stop the follower 456 and the Enchúfela", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Enchúfela doble y quédate", "", "Enchúfela, Enchúfela, Ronde", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Enchúfela el Alarde", "", "",
                "https://www.youtube.com/watch?v=krpmwN1nfXM"),
            new PlaylistItem(ItemType.Default, Format.Default, "Sombrero doble", "",
                "Sombrero 123 456, sombrero to mans left side. Then single sombrero back to mans right side, dile que no",
                "https://www.youtube.com/watch?v=vX7zMTs8eJA"),
            new PlaylistItem(ItemType.Default, Format.Default, "Bayamo", "",
                "Single hand sombrero. 456 around the man. 456 man turn anticlock-wise to woman right hand over head. Setenta to dile que no",
                "https://www.youtube.com/watch?v=lFaJ4W0LfCM"),
            new PlaylistItem(ItemType.Default, Format.Default, "Bayamo por abajo", "Bayamo below",
                "Single hand sombrero. 456 around the man. 123 left hand arm lock 456 come out. 123456 sombrero and hat. Dile que no.",
                "https://www.youtube.com/watch?v=KO9y_5HyWKg"),
            new PlaylistItem(ItemType.Default, Format.Default, "Montana", "Eighty-four",
                "Two hand no hat sombrero (123456). Anti clockwise 123 woman. 456 outside turn for man. Then sombrero y dile que no",
                ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Ochenta y quatro", "Eighty-four",
                "Two hand no hat sombrero (123456). Anti clockwise 123 woman. 456 outside turn for man. Then quédate to draw follower",
                "https://www.youtube.com/watch?v=GYzMYLR6NHg"),
            new PlaylistItem(ItemType.Default, Format.Default, "El uno", "The one", "", "https://www.youtube.com/watch?v=FZAvKSU2I7E"),
            new PlaylistItem(ItemType.Default, Format.Default, "La complete", "", "The complete set of Enchúfelas", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Puerto", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Club taco", "", "Sombrero start, catch elbow 123 - hat 567, prep 1 back over head 23, ronde 567, prep 123 two hand spin left 567, prep 123 two hand spin right hat 567, dile que no",
                "https://www.youtube.com/watch?v=W_kXoJvl-qA"),
            new PlaylistItem(ItemType.Default, Format.Default, "Coca cola", "", "Shape like the coca cola bottle",
                "https://youtu.be/Y_iN6x5z7Uc"),
            new PlaylistItem(ItemType.Default, Format.Default, "Ochos", "Eight", "", "https://youtu.be/wIZc1URrHMo"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Ochos: left handed and Enchúfela", "", "",
                "https://youtu.be/suSqLQbBRPs"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Ochos: left handed and complicado", "", "",
                "https://youtu.be/_YIC9pwdKs8"),
            new PlaylistItem(ItemType.Default, Format.Default, "Kentucky", "",
                "Cuddle, then cuddle to neck, then over head man turn clockwise to finish with dile que no",
                "https://www.youtube.com/watch?v=sK1SlAIE0LA"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Setenta", "Seventy", "", "https://youtu.be/5PB_sh_qu7A"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Setenta Tresario", "Seventy-three", "", "https://youtu.be/2Kk43iStQXQ"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Arco iris", "Rainbow",
                "Hammerlock, Enchúfela, Circle the left, Right across face, back to back, right / left, right / left, Stretch (follower turns right), alarde (left hand reverse), Enchúfela, alarde (right hand) - Setenta (123456), 123 unwind woman, 456 man in sweet heart position. 123 la Cruz. 456 Mans left hand up turns clockwise and woman clockwise.",
                "https://youtu.be/qXw-impfp5k"),
            new PlaylistItem(ItemType.Default, Format.Default, "4 corners?", "", "", ""),

            new PlaylistItem(ItemType.Default, Format.Default, "Guapea", "", "", "https://www.youtube.com/watch?v=YwxB1MSytYA"),
            new PlaylistItem(ItemType.Default, Format.Default, "Crisscross", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Push and throw", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Photo", "", "Pose for a photo", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Media", "", "Middle clap on 1", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Short cut sombrero con mambo", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Tumba francesa", "",
                "Right hand, Enchúfela, right hand, left hand, right hand, left hand behind back, Enchúfela damit", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Tira la sabana", "", "", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Dame", "", "Pass partner on", ""),
            new PlaylistItem(ItemType.Default, Format.Default, "Dame Duo", "", "Pass partner on, skip", ""),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Week 7", "", "", "https://youtu.be/dljQBg9QTL8"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Week 6", "", "", "https://youtu.be/eSzx9SdieZg"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Coffee Shop", "", "", "https://youtu.be/NH2rVg3eaU4"),
            new PlaylistItem(ItemType.Default, Format.Default, "Funny ", "", "", "https://www.youtube.com/watch?v=XzsFMy8yQ48"),
            new PlaylistItem(ItemType.Default, Format.Default, "Cuddle Turn variation", "", "",
                "https://www.youtube.com/watch?v=R8aZMRdMa6g"),
            new PlaylistItem(ItemType.HyperLink, Format.Default, "Rueda calls", "", "", "http://www.salsaclass.co.uk/rueda_calls.htm"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Bachata Combo - Cha Cha Block", "", "", "https://youtu.be/yzvTGFg80Hc"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Bachata Combo - Pretzel, Wave ", "", "",
                "https://youtu.be/HwZKuDGsn_M"),
            new PlaylistItem(ItemType.Default, Format.Mobile, "Bachata Combo - The Matrix", "", "", "https://youtu.be/TXkVrX3TRsw"),

        }
    };
}