using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SDTCore 
{
    public static class ComponentExtensionTest
    {
        /// <summary>
        /// Checks if the given component has any children.
        /// </summary>
        /// <param name="component">The component to check for children.</param>
        /// <returns>True if the component has children, otherwise false.</returns>
        public static bool HasChildren(this Component component) 
            => component.gameObject.transform.childCount > 0;

        /// <summary>
        /// Destroys game object with delay
        /// </summary>
        /// <param name="component">The component which game object is going to be destroyed</param>
        /// <param name="delay">Delay before destruction in seconds</param>
        public static async void DestroyGameObject(this Component component, float delay = 0)
        {
            await Task.Delay((int) delay * 1000);
            Object.Destroy(component.gameObject);
        }
        
        /// <summary>
        /// Destroy component with delay
        /// </summary>
        /// <param name="component">The component to be destroyed</param>
        /// <param name="delay">Delay before destruction in seconds</param>
        public static async Task Destroy(this Component component, float delay = 0)
        {
            await Task.Delay((int) delay * 1000);
            Object.Destroy(component);
        }
        
        /// <summary>
        /// Destroys all children of the given component.
        /// </summary>
        /// <param name="component">The component whose children should be destroyed.</param>
        public static void DestroyChildren(this Component component)
        {
            foreach (Transform tr in component.transform) 
                Object.Destroy(tr.gameObject);
        }
        
        /// <summary>
        /// Retrieves children of the given component.
        /// </summary>
        /// <param name="component">The component whose children should be retrieved.</param>
        /// <returns>A collection of children transforms.</returns>
        public static IEnumerable<Transform> GetChildren(this Component component) 
            => component.transform.Cast<Transform>();

        /// <summary>
        /// Retrieves children of the given component as RectTransform instances.
        /// </summary>
        /// <param name="component">The component whose children should be retrieved.</param>
        /// <returns>A collection of RectTransform instances representing the children.</returns>
        public static IEnumerable<RectTransform> GetChildrenRc(this Component component)
        {
            var listChildren = new List<RectTransform>();
            
            for (var i = 0; i < component.gameObject.transform.childCount; i++)
            {
                var child = component.gameObject.transform.GetChild(i);
                var childComponent = child.GetComponent<RectTransform>();
                
                if (childComponent) 
                    listChildren.Add(childComponent); 
            }
            return listChildren;
        }
        
        /// <summary>
        /// Retrieves children of the given component of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the children components to retrieve.</typeparam>
        /// <param name="component">The component whose children should be retrieved.</param>
        /// <returns>An enumerable collection of components of the specified type.</returns>
        public static IEnumerable<T> GetChildren<T>(this Component component) where T : Component
        {
            var listChildren = new List<T>();

            for (var i = 0; i < component.gameObject.transform.childCount; i++)
            {
                var child = component.gameObject.transform.GetChild(i);
                var childComponent = child.GetComponent<T>();
                
                if (childComponent) 
                    listChildren.Add(childComponent); 
            }
            return listChildren;
        }
        
        /// <summary>
        /// Destroys all children of the given component immediately, bypassing the usual Unity lifecycle.
        /// </summary>
        /// <param name="component">The component whose children should be destroyed immediately.</param>
        public static void DestroyChildrenImmediate(this Component component)
        {
            var childrenArray = new GameObject[component.transform.childCount];

            for (var i = 0; i < childrenArray.Length; i++) 
                childrenArray[i] = component.transform.GetChild(i).gameObject;
         
            foreach (var t in childrenArray) 
                Object.DestroyImmediate(t);
        }
        
        /// <summary>
        /// Destroys children of the given component of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the children components to destroy.</typeparam>
        /// <param name="component">The component whose children should be destroyed.</param>
        public static void DestroyChildren<T>(this Component component) where T : Component
        {
            for (var i = 0; i < component.gameObject.transform.childCount; i++)
            {
                var child = component.gameObject.transform.GetChild(i);
                
                if (child.GetComponent<T>()) 
                    Object.Destroy(child.gameObject);
            }
        }
        
        /// <summary>
        /// Destroys children of the given component except those of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the children components to preserve.</typeparam>
        /// <param name="component">The component whose children should be selectively destroyed.</param>
        public static void DestroyChildrenExcept<T>(this Component component) where T : Component
        {
            for (var i = 0; i < component.gameObject.transform.childCount; i++)
            {
                var child = component.gameObject.transform.GetChild(i);
                
                if (!child.GetComponent<T>())
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }
        
        /// <summary>
        /// Destroys children of the given component, except those associated with a specified Transform.
        /// </summary>
        /// <param name="component">The component whose children should be selectively destroyed.</param>
        /// <param name="except">The Transform whose associated children should be preserved.</param>
        public static void DestroyChildrenExcept(this Component component, Transform except)
        {
            for (var i = 0; i < component.gameObject.transform.childCount; i++)
            {
                var child = component.gameObject.transform.GetChild(i);
                
                if (child.transform != except.transform) 
                    Object.Destroy(child.gameObject);
            }
        }
        
        /// <summary>
        /// Destroys children of the given component, except those associated with specified Transforms.
        /// </summary>
        /// <param name="component">The component whose children should be selectively destroyed.</param>
        /// <param name="except">The Transforms whose associated children should be preserved.</param>
        public static void DestroyChildrenExcept(this Component component, params Transform[] except)
        {
            for (var i = 0; i < component.gameObject.transform.childCount; i++)
            {
                foreach (var t in except) 
                {
                    var child = component.gameObject.transform.GetChild(i);
                    
                    if (child.transform != t) 
                        Object.Destroy(child.gameObject);
                }
            }
        }
        
        /// <summary>
        /// Sets the active state of all children of the given component.
        /// </summary>
        /// <param name="component">The component whose children' active state should be set.</param>
        /// <param name="active">The active state to be set. Default is true.</param>
        public static void SetChildrenActive(this Component component, bool active = true)
        {
            foreach (Transform tr in component.transform) 
                tr.gameObject.SetActive(active);
        }
        
        /// <summary>
        /// Performs a specified action on each child of the given component that matches the provided type.
        /// </summary>
        /// <typeparam name="T">The type of the children to iterate over.</typeparam>
        /// <param name="component">The component whose children should be iterated over.</param>
        /// <param name="action">The action to be performed on each child of the specified type.</param>
        public static void ForeachChildren<T>(this Component component, Action<T> action) where T : Transform
        {
            foreach (T tr in component.transform) 
                action(tr);
        }
        
        /// <summary>
        /// Performs a specified action on each child of the given component, except for the provided Transform.
        /// </summary>
        /// <param name="component">The component whose children should be iterated over.</param>
        /// <param name="except">The Transform to be excluded from the iteration.</param>
        /// <param name="action">The action to be performed on each child except the specified Transform.</param>
        public static void ForeachChildrenExcept(this Component component, Transform except, Action<Transform> action) 
        {
            foreach (Transform transform in component.transform)
                if (transform != except) 
                    action(transform);
        }
        
        /// <summary>
        /// Performs a specified action on each child of the given component.
        /// </summary>
        /// <param name="component">The component whose children should be iterated over.</param>
        /// <param name="action">The action to be performed on each child.</param>
        public static void ForeachChildren(this Component component, Action<Transform> action)
        {
            foreach (Transform transform in component.transform) 
                action(transform);
        }
        
        /// <summary>
        /// Performs a specified action on each child of the given component, providing the child and its index to the action.
        /// </summary>
        /// <param name="component">The component whose children should be iterated over.</param>
        /// <param name="action">The action to be performed on each child along with its index.</param>
        public static void ForeachChildren(this Component component, Action<Transform, int> action)
        {
            var i = 0;
            foreach (Transform child in component.transform) 
                action(child, i++);
        }

        /// <summary>
        /// Gets an existing component of type T from the given component's game object, or adds one if none exists.
        /// </summary>
        /// <typeparam name="T">The type of component to get or add.</typeparam>
        /// <param name="component">The component whose game object should be used to get or add the component.</param>
        /// <returns>The existing or newly added component of type T.</returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component 
            => component.gameObject.TryGetComponent<T>(out var t) ? t : component.gameObject.AddComponent<T>();

        /// <summary>
        /// Gets an existing component of type T from the given game object, or adds one if none exists.
        /// </summary>
        /// <typeparam name="T">The type of component to get or add.</typeparam>
        /// <param name="gameObject">The game object from which to get or add the component.</param>
        /// <returns>The existing or newly added component of type T.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component 
            => gameObject.TryGetComponent<T>(out var t) ? t : gameObject.AddComponent<T>();
    }
}
