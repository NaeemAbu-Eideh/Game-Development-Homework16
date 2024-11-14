using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngineInternal;
using static UnityEngine.GraphicsBuffer;


namespace Assignment18 {

    public struct Position
    {
        public float X;
        public float Y;
        public float Z;
        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void PrintPosition() {
            Debug.Log($"the position in (X,Y,Z) is: ({X}, {Y}, {Z})");
        }
    }

    public class Character { 
        public string name;
        public int health;

        public Position position;

        public Character(string n, int h, Position p) {
            this.name = n;
            health = h;
            this.position = p;

        }
        public Character() : this("No name", 100, new Position(0, 0, 0)) { }
        
        public int Health
        {
            get { return health; }

            set {  health = Math.Clamp(value, 0, 100); }

        }

        public virtual void DisplayInfo()
        {
            Debug.Log($" Character name: {name}, Character Health: {Health}");
            position.PrintPosition();
        }

        public void Attack(int damage, Character target)
        {
            ApplyDamage(target, damage);
        }

        public void Attack(int damage, Character target, string attackType)
        {
            Debug.Log($"Attack Type: {attackType}");
            ApplyDamage(target, damage);
        }

        private void ApplyDamage(Character target, int damage)
        {
            target.Health -= damage;
            Debug.Log($"{target.name} has {target.Health} health remaining.");
        }
    }

    public class Soldier : Character
    {
        public Soldier(string name, int health, Position position) : base(name, health, position) { }
        public Soldier() : base() { }

        public override void DisplayInfo()
        {
            Debug.Log("Soldier");
            base.DisplayInfo();
        }
    }

    public class Officer : Character
    {
        public Officer(string name, int health, Position position) : base(name, health, position) { }

        public override void DisplayInfo()
        {
            Debug.Log("Officer");
            base.DisplayInfo();
        }
    }



    public class GameCode : MonoBehaviour
    {
        void Start()
        {
            Character[] characters = new Character[2];
            characters[0] = new Soldier();
            characters[1] = new Officer("Officer Joe", 90, new Position(10, 5, 2));

            foreach (Character character in characters)
            {
                character.DisplayInfo();
            }

            Soldier soldier = characters[0] as Soldier;
            Officer officer = characters[1] as Officer;

            Debug.Log($"Soldier's Health before attack: {soldier.Health}");
            officer.Attack(20, soldier, "shooting");
            Debug.Log($"Soldier's Health after attack: {soldier.Health}");
        }
    }

}



