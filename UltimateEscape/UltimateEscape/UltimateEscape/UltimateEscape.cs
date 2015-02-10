using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class UltimateEscape : PhysicsGame
{
    Image taustakuva = LoadImage("taistis");
  
    public override void Begin()
    {
        Level.Background.Color = Color.White;
        Level.Background.Image = taustakuva;
      
        Level.Background.Width = Screen.Width;
        Level.Background.Height = Screen.Height;
        luovalikko();
        LuoPistelaskuri();

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
        PhysicsObject tikkuukko = new PhysicsObject(40, 50);
     
        tikkuukko.Shape = Shape.Rectangle;
        Add(tikkuukko);
    }

    void luovalikko()
    {
        MultiSelectWindow alkuValikko = new MultiSelectWindow("Ultimate Escape",
    "Aloita peli",  "Lopeta");
        Add(alkuValikko);
        alkuValikko.AddItemHandler(0, aloitapeli);
        
        alkuValikko.AddItemHandler(1, Exit);
       
    }
    void luoteksti()
{

    

}
    void aloitapeli()
    {
        Level.Background.Image=null;
 ColorTileMap ruudut = ColorTileMap.FromLevelAsset("mappi");
        Camera.Zoom(0.6);
        ruudut.SetTileMethod(Color.Black, LuoKentta);
        ruudut.SetTileMethod(Color.Green, luopelaaja);
ruudut.Execute(20, 20);
   
    
    
    }
    void LuoPistelaskuri()
    {
        pisteLaskuri = new IntMeter(30);

        Label pisteNaytto = new Label();
        pisteNaytto.X = Screen.Left + 100;
        pisteNaytto.Y = Screen.Top - 100;
        pisteNaytto.TextColor = Color.Black;
        pisteNaytto.Color = Color.White;
        pisteNaytto.Title = "Elämiä Jäljellä";
        pisteNaytto.BindTo(pisteLaskuri);
        Add(pisteNaytto);
    }
    }




