namespace QHStudio.Game
{
    [System.Serializable]
    public enum ContactType
    {
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit,
        OnControllerColliderHit,
        OnEnter,
        OnStay,
        OnExit,
        OnCustom
    }
}