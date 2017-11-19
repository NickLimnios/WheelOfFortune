using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Helpers;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.Models.Wheels
{
    //When the Admin creates a wheel, this is what the DB will store.
    public class Wheel : IEntity
    {
        public int Id { get; set; }

        //only one wheel should be active. Marking any wheel Active(true) should marke all the others Inactive(false).
        public bool IsWheelActive { get; set; }

        //what happens with EF if below was a private ?? if it is invisible to EF then we could 
        //add a property WheelSliceContainer[] that will temporary hold the wheel slices that the admin is creating.
        //public List<WheelSliceContainer> UnderCreationWheel_Slices { get; set; }

        //All the slices of the wheel with their attributes stored as a string in the DB.
        public string AllWheelSlices { get; set; }


        //This will provide the json to feed it to JS client
        public JsonResult GenerateWheelJson()
        {
            return WheelJsonHelper.CreateClientWheel_Json(AllWheelSlices);
        }

        //This will take the Admin's created wheel and store it to WheelJson property in order to store the object in DB
        //Admin Creates wheelSlices.
        //Admin wheelSlices.Save --> server.CreatedWheel = new wheel();
        //Server CreatedWheel.AllWheelSlices  = CreatedWheel.WheelSlices_ToString(wheelSlices); 
        //Server.dbContext.save(CreatedWheel) ;
        //Admin goto wheelUI and Select active Wheel.
        //Done
        public void WheelSlices_ToString(List<WheelSliceContainer> createdSlices)
        {
            AllWheelSlices = WheelJsonHelper.AppendWheelSlices_ToString(createdSlices);

        }

    }
}
