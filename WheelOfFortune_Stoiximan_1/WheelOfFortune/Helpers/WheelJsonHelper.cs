using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Models.Wheels;

namespace WheelOfFortune.Helpers
{
    //add stuff at top and bottom of json and segemtns in between. return the json ready for client consumption.
    public class WheelJsonHelper
    {
        public List<WheelSliceContainer> slices { get; set; }

        private const string WheelFooter1 = "\"colorArray\":[ \"#ce4314\", \"#0de5e5\", \"#180de5\", \"#e50d2e\", \"#0de51c\", \"#0d79e5\", \"#16A085\", \"#27AE60\", \"#2980B9\", \"#8E44AD\", \"#2C3E50\", \"#F39C12\", \"#D35400\", \"#C0392B\", \"#BDC3C7\",\"#1ABC9C\", \"#2ECC71\", \"#E87AC2\", \"#3498DB\", \"#9B59B6\", \"#7F8C8D\"]";
        private const string WheelFooter2 = "\"svgWidth\": 1024,  \"svgHeight\": 768,  \"wheelStrokeColor\": \"#D0BD0C\",  \"wheelStrokeWidth\": 18,  \"wheelSize\": 700,  \"wheelTextOffsetY\": 80,  \"wheelTextColor\": \"#EDEDED\",  \"wheelTextSize\": \"2.3em\",  \"wheelImageOffsetY\": 40,  \"wheelImageSize\": 50,  \"centerCircleSize\": 0,  \"centerCircleStrokeColor\": \"#F1DC15\",  \"centerCircleStrokeWidth\": 12,  \"centerCircleFillColor\": \"#EDEDED\",  \"segmentStrokeColor\": \"#E2E2E2\",  \"segmentStrokeWidth\": 4,  \"centerX\": 512,  \"centerY\": 384,  \"hasShadows\": false,  \"numSpins\": 999 ,  \"spinDestinationArray\":[],  \"minSpinDuration\":6,  \"gameOverText\":\"GAME OVER\",  \"invalidSpinText\":\"INVALID SPIN. PLEASE SPIN AGAIN.\",  \"introText\":\"YOU HAVE TO<br>SPIN IT <span style='color:#F282A9;'>2</span> WIN IT!\",  \"hasSound\":true,  \"gameId\":\"9a0232ec06bc431114e2a7f3aea03bbe2164f1aa\",  \"clickToSpin\":true";

        public static JsonResult CreateClientWheel_Json(string wheelSlices)
        {
            var resultingJson = AppendStrings_ToJsonFormat(new string[] { wheelSlices, WheelFooter1, WheelFooter2 });
            return new JsonResult(new
            {
                resultingJson
            });

        }

        private static string AppendStrings_ToJsonFormat(string[] strs)
        {
            //we get all the values add a "{ "before, a "," between them and a "}" after in order to create a Json ,then we return them.
            return "{" + strs.Aggregate((i, j) => i + "," + j) + "}";
        }

        //This is called after the admin creates a wheel to convert the wheel slices to string in order to be saved in the DB.
        public static string AppendWheelSlices_ToString(List<WheelSliceContainer> slicesToConvert)
        {
            //tried this with linq but failled...
            //would move the string in StaRes file.
            string allSlicesToString = "\"segmentValuesArray\" : [";
            for (int i = 0; i < slicesToConvert.Count; i++)
            {
                //concat and add a "," if not the last element.
                allSlicesToString += JsonConvert.SerializeObject(slicesToConvert[i]) + (i < slicesToConvert.Count - 1 ? "," : "");
            }

            allSlicesToString += "]";
            return allSlicesToString;

        }

        public static List<WheelSliceContainer> GetWheelSlicesFromString(string allSlices)
        {
            //would move the string in StaRes file.
            return new List<WheelSliceContainer>
                (JsonConvert.DeserializeObject<List<WheelSliceContainer>>(allSlices.Replace("\"segmentValuesArray\" :", "")));
            
        }
    }
}