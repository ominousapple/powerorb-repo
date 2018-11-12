public interface IControllable
{
    void MoveLeft_Down();
    void MoveRight_Down();
    void Jump_Down();
    void Attack_Down();
    void UseOrb_Down();
    void Interact_Down();

    void MoveLeft_Up();
    void MoveRight_Up();
    void Jump_Up();
    void Attack_Up();
    void UseOrb_Up();
    void Interact_Up();

    void HorizontalInput(float h_input);
    void VerticalInput(float v_input);

}