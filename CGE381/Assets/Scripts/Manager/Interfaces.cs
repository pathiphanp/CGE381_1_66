using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interfaces
{

}

public interface TakeDamage
{
    void TakeDamage(int damage, string dieReprot);
}
public interface DeleteChild
{
    void DeleteChild();
}
public interface AddChild
{
    void AddChild(GameObject child);
}


