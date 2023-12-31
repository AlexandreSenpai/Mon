using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
public class Node {

    private int x;
    private int y;

    private int f = 0;
    private int g = 0;
    private int h = 0;
    private Node parent = null;
    public bool canPassThrough = false;

    public bool isOnClosedList = false;
    public bool isOnOpenList = false;

    public Node(int x = 0, int y = 0) {
        this.x = x;
        this.y = y;
    }

    public static int Heuristic(Node start, Node end) {
        return Math.Abs(end.x - start.x) + Math.Abs(end.y - start.y);
    }

    public Node GetParent() {
        return this.parent;
    }

    public void SetParentNode(Node parent) {
        this.parent = parent;
    }

    private void CalculateFValue() {
        this.f = this.g + this.h;
    }

    public void SetGValue(int g) {
        this.g = g;
        this.CalculateFValue();
    }

    public void SetHValue(int h) {
        this.h = h;
        this.CalculateFValue();
    }

    public int GetFValue() {
        return this.f;
    }

    public int GetGValue() {
        return this.g;
    }

    public int GetX() {
        return this.x;
    }

    public int GetY() {
        return this.y;
    }

}
