using UnityEngine;

namespace Customize.Create
{
    public class Body : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        private void Start() { }

        public void Execute(UnitTeamData member)
        {
            Create(member);
            Setup();
        }

        private void Create(UnitTeamData member)
        {
            string path = $"Battle/Сharacters/{member.type}/Unit";

            Manager.unit = Instantiate(Resources.Load(path, typeof(GameObject)) as GameObject);

            Manager.unit.gameObject.name = member.type;
            Manager.unit.transform.parent = Manager.spawnPoint.transform;
            Manager.unit.transform.localPosition = new Vector3(0, 0, 0);

            Manager.weapon = Manager.unit.transform.Find("Weapon").gameObject;
            Manager.armor = Manager.unit.transform.Find("Armor").gameObject;
            //Manager.face = Manager.unit.transform.Find("Face").gameObject;
        }

        private void Setup() { }
    }
}
