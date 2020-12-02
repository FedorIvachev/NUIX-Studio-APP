namespace Tsinghua.HCI.IoThingsLab
{
    public interface IItem
    {
        /// <summary>
        /// returns the name of the item
        /// </summary>
        /// <returns>the name of the item</returns>
        string getName();

        /// <summary>
        /// returns the type of the item
        /// </summary>
        /// <returns>the type of the item</returns>
        string getType();

        /// <summary>
        /// returns the state of the item
        /// </summary>
        /// <returns>the state of the item</returns>
        string getState();
    }

}