using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    public class Machine : MonoBehaviour
    {
        [SerializeField] private Recipe[] recipes;
        [SerializeField] private Transform inputTriggerTransform;
        [SerializeField] private LayerMask inputLayerMask;
        [SerializeField] private Transform outputSpawnPoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                ProcessInputs();
        }

        private void ProcessInputs()
        {
            var collidersInInputZone = Physics.OverlapBox(
                inputTriggerTransform.position,
                inputTriggerTransform.lossyScale / 2, //LossyScale accounts for scale changes in the entire GameObject's parent hierarchy.
                inputTriggerTransform.rotation,
                inputLayerMask);

            var foundIngredientContainers = new List<IngredientContainer>();

            foreach (var currentCollider in collidersInInputZone)
            {
                // Debug.Log($"Collider: {currentCollider.gameObject.name}");

                var ingredientContainer = currentCollider.GetComponentInParent<IngredientContainer>(); //Look for an Ingredient component in the collider's parents.
                if (ingredientContainer == null) //If no Ingredient component was found, continue to the next collider.
                    continue;

                if (foundIngredientContainers.Contains(ingredientContainer)) //If we've already added this ingredient, then continue. Don't add it again.
                    continue;

                foundIngredientContainers.Add(ingredientContainer);
            }

            var ingredientDictionary = new Dictionary<Ingredient, int>();

            //Count how many of each ingredient we found.
            foreach (var ingredientContainer in foundIngredientContainers)
            {
                // Debug.Log($"IngredientContainer: {ingredientContainer.gameObject.name}");

                var currentIngredient = ingredientContainer.Ingredient;

                if (ingredientDictionary.ContainsKey(currentIngredient)) //Does the dictionary already contain this ingredient? I.e. have we already added it?
                    ingredientDictionary[currentIngredient] += 1; //Add 1 to the count of this ingredient.
                else //If we haven't added this ingredient yet.
                    ingredientDictionary.Add(currentIngredient, 1); //Add the currentIngredient to the dictionary, and give the count 1.
            }

            //Check which recipes are valid.
            foreach (var recipe in recipes)
            {
                if (IsRecipeCraftable(recipe, ingredientDictionary))
                {
                    Craft(recipe);
                    break; //If this recipe is craftable, then only craft this one. Break let's us jump out of the foreach-loop early.
                }
            }
        }

        private bool IsRecipeCraftable(Recipe recipe, Dictionary<Ingredient, int> ingredientDictionary)
        {
            //Keep it simple for now. The first matching recipe that we find is the one that gets crafted.
            foreach (var ingredientCount in recipe.IngredientCounts)
            {
                var requiredIngredient = ingredientCount.Ingredient;
                var requiredCount = ingredientCount.Count;

                if (ingredientDictionary.ContainsKey(requiredIngredient) == false) //If a required ingredient wasn't found, then we can't craft this recipe.
                    return false;

                if (ingredientDictionary[requiredIngredient] < requiredCount) //If there are not enough ingredients of this type, then we can't craft this recipe.
                    return false;
            }

            //If we get to this point, then we know that the ingredient was found, and that there's enough of it. Then we can craft this recipe.
            return true;
        }

        private void Craft(Recipe recipe)
        {
            Debug.Log($"Crafting: {recipe.name}");

            Instantiate(recipe.Output, outputSpawnPoint.position, outputSpawnPoint.rotation);

            //TODO Remove used ingredient GameObjects. Will need to pass in the found ingredientContainers to do this.
        }
    }
}