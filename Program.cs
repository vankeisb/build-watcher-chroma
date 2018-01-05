using System;

using Corale.Colore.Core;
using ColoreColor = Corale.Colore.Core.Color;
using Key = Corale.Colore.Razer.Keyboard.Key;
using Newtonsoft.Json.Linq;
using KeyboardCustom = Corale.Colore.Razer.Keyboard.Effects.Custom;
using Corale.Colore.Razer.Keyboard;

namespace testdotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            String data = Environment.GetEnvironmentVariable("BUILD-WATCHER-DATA");
            Console.WriteLine("Data = " + data);

            JObject o = JObject.Parse(data);

            JArray tags = (JArray)o["tags"];
            var grid = KeyboardCustom.Create();
            grid.Clear();
            int index = 0;
            for (var r = 0; r < Constants.MaxRows; r++)
            {
                for (var c = 2; c < Constants.MaxColumns; c++)
                {
                    if (index < tags.Count) {
                        ColoreColor color;
                        var tag = tags[index];
                        string status = (string)tag["status"];
                        if (status == "Green") {
                            color = ColoreColor.Green;
                        } else if (status == "Red") {
                            color = ColoreColor.Red;
                        } else {
                            color = ColoreColor.Orange;
                        }
                        grid[r, c] = color;
                    }                    
                    index++;
                }
            }

            Chroma.Instance.Keyboard.Clear();
            Chroma.Instance.Keyboard.SetCustom(grid);

        //    Chroma.Instance.Keyboard.SetAll(ColoreColor.White);
            // Chroma.Instance.Keyboard.SetKey(Key.F1, ColoreColor.Green);
        //    Chroma.Instance.Keyboard.SetKey(Key.F2, ColoreColor.Red);            
        }
    }


}
