using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballLeague.Management.Models
{
    [Table("Teams")]
    public class Team
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamID { get; set; }

        [DisplayName("Team Name")]
        [MaxLength(20)]
        [Required(ErrorMessage = "You must provide a Team Name")]
        public string TeamName { get; set; }


        [MaxLength(20)]
        public string Place { get; set; }

        [DisplayName("Home Jersey Color")]
        [Required(ErrorMessage = "You must provide a Home Jersey Color")]
        [MaxLength(10)]
        public string HomeJerseyColor { get; set; }

        [DisplayName("Away Jersey Color")]
        [MaxLength(10)]
        [Required(ErrorMessage = "You must provide a Away Jersey Color")]
        public string AwayJerseyColor { get; set; }

        [DisplayName("Ground Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "You must provide a Ground")]
        public string GroundName { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Contact Number")]
        [MaxLength(20)]
        public string ContactNumber { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime Modified { get; set; }


    }
}