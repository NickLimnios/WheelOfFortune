using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models.Wheels
{
    public class WheelSliceContainer
    {
        /*CAUTION THE VARS HERE ARE CASE SENSITIVE AND ARE MAPPED TO THE Spin2WinWheel.min.js.
         * DO NOT REFACTOR THEM WITHOUT REFACTORING THEM THERE AS WELL*/

        //probability - Number - any integer E.g. "probability"=20. All the probability values will be added together. 
        //Their individual value will be a percentage of that total value.Similarly "probability"=0 means
        //the wheel will never land on this segment.Unlike previous versions these values no longer need to add up to 100.
        public int probability { get; set; }

        //type - String - the type of value on the segment - can be 'image' or 'string'.
        //You have the flexibility to use text or an image(SVG, PNG, JPG, GIF) - you can even use an animated GIF.
        //Its dimensions are determined by wheelImageSize.If the image you use is not square and the wheelImageSize is,
        //say, 54, the image will be scaled maintaining its aspect ratio to a width of 54px.SVG 1.1 
        //does not natively support word wrapping, so you can add a ^ where you want a new line.E.g.You won^a holiday!
        public string type { get; set; }

        //value - String - can be an image URL(SVG image is recommended for resolution independence) 
        //or a value like '$450' or 'Holiday'.
        public string value { get; set; }

        //win - Boolean - describes whether this segment is a winner or loser.Useful for both frontend display and backend DB
        public bool win { get; set; }

        //resultText - String - the text displayed when the wheel lands on the segment
        public string resultText { get; set; }

        //userData - Object - an optional object that can contain custom data for retrieval in the onResult event
        public WheelSlice_UserData userData { get; set; }
    }
}
