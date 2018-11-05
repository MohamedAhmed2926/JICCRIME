using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Crime.Repositories.Repositories
{
    public class TextPredictionsRepository : EntityRepositoryBase<CourtConfigurations_TextPredictions>, ITextPredictionsRepository
    {
        public AddTextStatus AddTextPredections(vw_TextPredections TextPredections)
        {
            try
            {
                var TitleExist = (from texts in DataContext.CourtConfigurations_TextPredictions
                                 // join crimes in DataContext.Cases_CrimeTypes on texts.CrimeTypeID equals crimes.ID
                                  join circuit in DataContext.CourtConfigurations_Circuits on texts.CircuitID equals circuit.ID
                                 // join users in DataContext.Users on circuit.SecretaryID equals users.Id
                                  where texts.CircuitID == TextPredections.CircuitID && texts.Title == TextPredections.TextTitle
                                  select texts.ID).Any();
                if (TitleExist)
                {
                    return AddTextStatus.SameTitle;
                }
                else
                {
                    CourtConfigurations_TextPredictions obj = new CourtConfigurations_TextPredictions();
                    obj.CircuitID = TextPredections.CircuitID;
                    obj.Title = TextPredections.TextTitle;
                    obj.Phrase = TextPredections.TextPredectionsDescription;
                    //obj.UserID = TextPredections.UserID;

                    this.Add(obj);
                    this.Save();
                    return AddTextStatus.AddSuccefull;
                }

            }
            catch (Exception)
            {
                return AddTextStatus.FailedToAdd;
            }
        }

        public bool DeleteTextByID(int TextID)
        {
            try
            {
                var prediction = this.GetByID(TextID);
                this.Delete(prediction);
                this.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public EditTextStatus EditText(vw_TextPredections TextPredections)
        {
            try
            {
                var TitleExist = (from texts in DataContext.CourtConfigurations_TextPredictions
                                 // join crimes in DataContext.Cases_CrimeTypes on texts.CrimeTypeID equals crimes.ID
                                  join circuit in DataContext.CourtConfigurations_Circuits on texts.CircuitID equals circuit.ID
                                 // join users in DataContext.Users on circuit.SecretaryID equals users.Id
                                  where texts.CircuitID == TextPredections.CircuitID && texts.Title == TextPredections.TextTitle && texts.ID != TextPredections.TextID 
                                  select texts.ID).Any();
                if (TitleExist)
                {
                    return EditTextStatus.SameTitle;
                }
                else
                {
                    var result = this.GetByID(TextPredections.TextID);
                    result.ID = TextPredections.TextID;
                    result.CircuitID = TextPredections.CircuitID;
                   // result.UserID = TextPredections.UserID;
                    result.Title = TextPredections.TextTitle;
                    result.Phrase = TextPredections.TextPredectionsDescription;
                    this.Update(result);
                    this.Save();
                    return EditTextStatus.EditSuccefull;
                }

            }
            catch (Exception ex)
            {
                return EditTextStatus.FailedToEdit;
            }
        }
       
            public List<vw_KeyValue> GetCrimeTypes(int UserId)
        {
           // List<vw_KeyValue> result;

          //  var usertype = (from users in DataContext.Users where users.Id == UserId select users.UserTypeID).FirstOrDefault();

         //   if (usertype == (int)SystemUserTypes.Secretary)
         //   {
            //    var usercircuits = (from ciruits in DataContext.CourtConfigurations_CircuitMembers where ciruits.UserID == UserId
                //                    select ciruits.CircuitID);
            
            var    result = (from types in DataContext.Cases_CrimeTypes
                          join circuit in DataContext.CourtConfigurations_Circuits on types.ID equals circuit.CrimeType
                         // join member in DataContext.CourtConfigurations_CircuitMembers on circuit.ID equals member.CircuitID
                          join users in DataContext.Users on circuit.SecretaryID equals users.Id
                          where/* usercircuits.Contains(circuit.ID) &&*/ users.Id == UserId

                          select new vw_KeyValue
                          {
                              ID = types.ID,
                              Name = types.Name
                          }).Distinct().ToList();
           // }
           // else
           //{
           //     result = (from types in DataContext.Cases_CrimeTypes
           //               select new vw_KeyValue
           //               {
           //                   ID = types.ID,
           //                   Name = types.Name
           //               }).ToList();
           // }
            return result;
        }

       
        public vw_TextPredections GetTextByID(int TextID)
        {
            return (from predictions in DataContext.CourtConfigurations_TextPredictions
                    join Circuit in DataContext.CourtConfigurations_Circuits on predictions.CircuitID equals Circuit.ID
                    where predictions.ID == TextID
                    select new vw_TextPredections
                    {
                        TextID = predictions.ID,
                        CircuitID = predictions.CircuitID,
                        CircuitName = Circuit.Name,
                        TextTitle = predictions.Title,
                        TextPredectionsDescription = predictions.Phrase
                    }).FirstOrDefault();
        }

        public List<vw_TextPredections> GetTextPredections(List<vw_KeyValue> CircuitID)
        {

        var result1=(from predictions in DataContext.CourtConfigurations_TextPredictions
                 //  join crimes in DataContext.Cases_CrimeTypes on predictions.CrimeTypeID equals crimes.ID
             join circuit in DataContext.CourtConfigurations_Circuits on predictions.CircuitID equals circuit.ID
             // join member in DataContext.CourtConfigurations_CircuitMembers on circuit.ID equals member.CircuitID
             //  join users in DataContext.Users on circuit.SecretaryID equals users.Id
             /* usercircuits.Contains(circuit.ID) && users.UserID == userID*/
         
             //  ((predictions.UserID == userID && userID.HasValue) || !userID.HasValue)
             //  && ((predictions.CrimeTypeID == CrimeTypeID && CrimeTypeID.HasValue) || !CrimeTypeID.HasValue)
             select new vw_TextPredections
             {
                 TextID = predictions.ID,
                 CircuitID = predictions.CircuitID,
                 CircuitName = circuit.Name,
                 TextTitle = predictions.Title,
                 TextPredectionsDescription = predictions.Phrase
             }).ToList();

            var result2= (from predictions in result1
                          where CircuitID.Select(e=>e.ID).Contains(predictions.CircuitID)
                         select new vw_TextPredections
                         {
                             TextID = predictions.TextID,
                             CircuitID = predictions.CircuitID,
                             CircuitName = predictions.CircuitName,
                             TextTitle = predictions.TextTitle,
                             TextPredectionsDescription = predictions.TextPredectionsDescription
                         }).ToList();
            return result2;

        }
    }
}
