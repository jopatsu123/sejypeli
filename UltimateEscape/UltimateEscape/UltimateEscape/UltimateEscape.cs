using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class UltimateEscape : PhysicsGame
{
    public override void Begin()
    {
        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("mappi");
        ruudut.SetTileMethod(Color.Black, LuoKentta);
        ruudut.SetTileMethod(Color.Green, luopelaaja);

        ruudut.Execute(20, 20);
        

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LuoKentta(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject mappi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        mappi.Position = paikka;
        Add(mappi);
        mappi.Color=Color.Black;
        Level.Background.Color = Color.White;
    }
    void luopelaaja(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject tikkuukko = new PhysicsObject(40, 50);
    }
}
