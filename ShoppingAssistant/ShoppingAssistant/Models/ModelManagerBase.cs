using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ShoppingAssistant.APIClasses;

namespace ShoppingAssistant.Models
{
    public class ModelManagerBase<T> where T : Model
    {
        public ObservableCollection<T> Models;

        public ModelManagerBase

        public void AddModel(IEnumerable<T> models)
        {
            if (models != null)
            {
                foreach (var newModel in models)
                {
                    if (newModel.Deleted)
                    {
                        this.DeleteShoppingListAsync(newModel);
                        break;
                    }

                    var oldList = this.Models.FirstOrDefault(l => l.RemoteDbId == newModel.RemoteDbId);
                    if (oldList == null)
                    {
                        // Insert the list as no list with the same RemoteDbId could be found
                        this.Models.Add(newModel);
                    }
                    else if (RubyDateParser.Compare(oldList.LastUpdated, newModel.LastUpdated) < 0)
                    {
                        // Replace the old list with the new if it was last updated more recently
                        oldList = newModel;
                    }
                }
            }
        }

        public async void DeleteModelAsync(T model)
        {
            this.Models.Remove(model);

            model.Deleted = true;
            
            // Save this item to the database as deleted so that it can be deleted if the api call fails
            databaseHelper.SaveShoppingListAsync(model);

            bool deleted = await apiHelper.DeleteShoppingListModelAsync(model);

            if (deleted) databaseHelper.DeleteShoppingListAsync(model);
        }
    }
}
