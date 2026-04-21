var rotateSpeed : int;
//Very simple transform y rotation at desired speed
function Update () {
     transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
}
