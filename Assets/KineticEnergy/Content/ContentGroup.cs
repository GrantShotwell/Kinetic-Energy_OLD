using System;
using System.Reflection;

using KineticEnergy.Enumeration;
using KineticEnergy.Entities;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Intangibles.Behaviours;

using UnityEngine;

using static KineticEnergy.Content.ContentAttributes;

namespace KineticEnergy.Content {

    public class ContentGroup {

        /// <summary>The name of this group. Usually the mod name.</summary>
        public string Name { get; }

        public Properties<ContentList> ContentLists { get; }

        public ContentList<GlobalBehaviour> Globals { get; } = new ContentList<GlobalBehaviour>();
        public ContentList<ClientBehaviour> Clients { get; } = new ContentList<ClientBehaviour>();
        public ContentList<ServerBehaviour> Servers { get; } = new ContentList<ServerBehaviour>();
        public ContentList<BlockPreview> Blocks { get; } = new ContentList<BlockPreview>();
        public ContentList<Entity> Entities { get; } = new ContentList<Entity>();
        public ContentList<AttachedBehaviour> Attachables { get; } = new ContentList<AttachedBehaviour>();
        public ContentList<MonoBehaviour> Monos { get; } = new ContentList<MonoBehaviour>();

        /// <summary>Creates a new <see cref="ContentGroup"/> with the given properties.</summary>
        /// <param name="origin">The value of <see cref="Name"/>.</param>
        public ContentGroup(string origin) {
            Name = origin;
            ContentLists = new Properties<ContentList>(Globals, Clients, Servers, Blocks, Entities, Attachables, Monos);
        }

        public bool GetList<T>(out ContentList<T> list) where T : class => (list = GetList(typeof(T), out ContentList _list) ? _list as ContentList<T> : null) != null;

        public bool GetList(Type type, out ContentList list) {

            if(typeof(GlobalBehaviour).IsAssignableFrom(type)) {
                list = Globals;
                return true;
            }

            if(typeof(ClientBehaviour).IsAssignableFrom(type)) {
                list = Clients;
                return true;
            }

            if(typeof(ServerBehaviour).IsAssignableFrom(type)) {
                list = Servers;
                return true;
            }

            if(typeof(BlockPreview).IsAssignableFrom(type)) {
                list = Blocks;
                return true;
            }

            if(typeof(Entity).IsAssignableFrom(type)) {
                list = Entities;
                return true;
            }

            if(typeof(AttachedBehaviour).IsAssignableFrom(type)) {
                list = Attachables;
                return true;
            }

            if(typeof(MonoBehaviour).IsAssignableFrom(type)) {
                list = Monos;
                return true;
            }

            list = null;
            return false;

        }

        /// <summary>Adds the given <see cref="Content"/> to the associated item in <see cref="ContentLists"/>.</summary>
        /// <param name="content">The <see cref="Content"/> to add.</param>
        /// <param name="found">The item in <see cref="ContentLists"/> that the <paramref name="content"/> was added to.</param>
        /// <returns>Returns true if a correct <see cref="ContentList"/> was found.</returns>
        public bool Add(Content content, out ContentList found) {
            if(GetList(content.Type, out ContentList contentList)) {
                contentList.Add(content);
                found = contentList;
                return true;
            } else {
                found = null;
                return false;
            }
        }

        /// <summary>Adds the given <see cref="Content{T}"/> to the associated item in <see cref="ContentLists"/>.</summary>
        /// <param name="content">The <see cref="Content{T}"/> to add.</param>
        /// <param name="found">The item in <see cref="ContentLists"/> that the <paramref name="content"/> was added to.</param>
        /// <returns>Returns true if a correct <see cref="ContentList"/> was found.</returns>
        public bool Add<T>(Content<T> content, out ContentList<T> found) where T : class {
            if(GetList(out ContentList<T> contentList)) {
                contentList.Add(content);
                found = contentList;
                return true;
            } else {
                found = null;
                return false;
            }
        }

        /// <summary>Removes the <see cref="Content{T}"/> with the given <see cref="Content{T}.Type"/>.</summary>
        /// <returns>Returns true if an item was found and removed.</returns>
        /// <seealso cref="Remove{T}(out ContentList)"/>
        public bool Remove(Type type, out ContentList found) {
            if(GetList(type, out ContentList contentList)) {
                found = contentList;
                var genericList = contentList.GenericList;
                foreach(var item in genericList)
                    if(item.Type.Equals(type))
                        return true;
                return false;
            } else {
                found = null;
                return false;
            }
        }

        /// <summary>Removes the <see cref="Content{T}"/> with the given <see cref="Content{T}.Type"/>.</summary>
        /// <remarks>Slightly better than <see cref="Remove(Type, out ContentList)"/>.</remarks>
        /// <returns>Returns true if an item was found and removed.</returns>
        public bool Remove<T>(out ContentList found) where T : class {
            if(GetList(out ContentList<T> contentList)) {
                found = contentList;
                var genericList = contentList.GenericList;
                foreach(var item in genericList)
                    if(item.Type.Equals(typeof(T)))
                        return true;
                return false;
            } else {
                found = null;
                return false;
            }
        }

        /// <summary>Add that to this.</summary>
        public void Add(ContentGroup that) {

            foreach(ContentList thatList in that.ContentLists) {
                GetList(thatList.Type, out ContentList thisList);

                //thisList = list of this object
                //thatList = list of param group

                foreach(Content thatItem in thatList.GenericList) {
                    foreach(ContentAttribute attribute in thatItem.Type.GetCustomAttributes<ContentAttribute>()) {

                        //ReplaceType
                        if(attribute is ReplacesTypeAttribute replaceAttribute) {
                            foreach(Content thisItem in thisList.GenericList) {
                                if(thisItem.Type.Equals(replaceAttribute.ReplacedType)) {
                                    thisList.GenericList.Remove(thisItem);
                                    break;
                                }
                            }
                        }

                        // ...

                    }

                    //Add the item.
                    thisList.Add(thatItem);

                }
            }

        }

    }

}
