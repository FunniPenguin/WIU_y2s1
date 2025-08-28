using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Actions/PatrolAction")]
public class PatrolAction2 : StateAction
{
    public float patrolSpeed = 2f;
    private int currentWaypoint = 0;

    public override void Act(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data.waypoints.Length == 0) return;

        Transform wp = data.waypoints[currentWaypoint];
        controller.transform.position = Vector2.MoveTowards(controller.transform.position,wp.position,patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(controller.transform.position, wp.position) < 0.2f)
            currentWaypoint = (currentWaypoint + 1) % data.waypoints.Length;
    }
}
// Made by Jovan Yeo Kaisheng
