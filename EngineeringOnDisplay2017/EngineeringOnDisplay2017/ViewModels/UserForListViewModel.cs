/****************************************************************************************************************
 * Project: Engineering on Display
 * Purpose: Website for displaying sensor data from the Engineering and Industry Building at the University of
 *          Alaska, Anchorage.  First features is a front end website for the general users to view
 *          the graphical data of the sensors and read more on the purpose of the display.  Second Feature
 *          is a admin secured website to add more slideshows to the third feature and view statistics. The third feature
 *          is a slide show of different advertisements.  
 * 
 * Author:  Terrance Mount
 * 
 *           
 * Sponsor: Dr. Kenrick Mock
 * 
 * Instructor: Dr. Martin Cenek
 * 
 * Class:  CSCE 470 Capstone  Spring 2017
 * College: University of Alaska, Anchorage
 * ***********************************************************************************************************************
 * File: UserListViewModel.cs
 * Purpose: View model to use with razor to render a list of users in the database. Simplifies the Identity table from 
 *          the database.
 * *******************************************************************************************************************
 * Date Created: 7/21/2017 by Terrance
 *      -Created the properties needed for the model.  
 *      -Added validation needed for creating a new user.
 * ********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EngineeringOnDisplay2017.ViewModels
{
    //class used as a model for the UserModel.cs API
    public class UserForListViewModel
    {
        //Username for the user
        public string Username { get; set; }

        //First and last name of user
        public string Name { get; set; }

        //How much access the user has : Admin == All,  Limited == View Only
        public string Role { get; set; }

        //Link to the details for the user
        public string DetailsLink { get; set; }

        //Link to delete the user
        public string DeleteUserLink { get; set; }
    }
}
