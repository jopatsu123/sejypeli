using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class UltimateEscape : PhysicsGame
{
    double nopeusYlos=200; 
    double nopeusAlas =200; 
    double nopeusVasemmalle=200;
    double nopeusOikealle = -200;
    PlatformCharacter pelaaja;
    Image taustakuva = LoadImage("taistis");
    Image tikkuukko = LoadImage("oik");  
  
    public override void Begin()
    {
        Gravity = new Vector(0.0, -800.0);
        Level.Background.Color = Color.White;
        Level.Background.Image = taustakuva;
      
        Level.Background.Width = Screen.Width;
        Level.Background.Height = Screen.Height;
        luovalikko();
        LuoPistelaskuri();
        luocontrors();

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LuoKentta(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject mappi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        mappi.Position = paikka;
        

        Add(mappi);
        mappi.Color=Color.Black;
       
    }
    void luopelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja = new PlatformCharacter(100, 100);
        pelaaja.Image = tikkuukko;
        pelaaja.Position = paikka;
        Add(pelaaja);
        
    }

    void luovalikko()
    {
        MultiSelectWindow alkuValikko = new MultiSelectWindow("Ultimate Escape",
    "Aloita peli",  "Lopeta");
        Add(alkuValikko);
        alkuValikko.AddItemHandler(0, aloitapeli);
        
        alkuValikko.AddItemHandler(1, Exit);
       
    }
    void luocontrors()
    {
        Keyboard.Listen(Key.A, ButtonState.Down, LiikutaHahmoa, "liiku vasemmalle",nopeusOikealle);
       // Keyboard.Listen(Key.A, ButtonState.Released, LiikutaHahmoa, "liiku vasemmalle", Vector.Zero);

}
    void aloitapeli()
    {
        Level.Background.Image=null;
 ColorTileMap ruudut = ColorTileMap.FromLevelAsset("mappi");
        Camera.Zoom(0.6);
        ruudut.SetTileMethod(Color.Black, LuoKentta);
        ruudut.SetTileMethod(Color.FromHexCode("00FF21"), luopelaaja);
ruudut.Execute(20, 20);
   
    
    
    }
    void LuoPistelaskuri()
    {
       IntMeter pisteLaskuri = new IntMeter(30);

        Label pisteNaytto = new Label();
        pisteNaytto.X = Screen.Left + 100;
        pisteNaytto.Y = Screen.Top - 100;
        pisteNaytto.TextColor = Color.Black;
        pisteNaytto.Color = Color.White;
        pisteNaytto.Title = "Lives left";
        pisteNaytto.BindTo(pisteLaskuri);
        Add(pisteNaytto);
    }
    void LiikutaHahmoa(double nopeus)
    {
        pelaaja.Walk(nopeus); 




    }
    }




