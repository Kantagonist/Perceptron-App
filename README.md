# Perceptron-App

I am trying to teach myself some basics about machine learning.
This app generates a given amount of points with different colours on a 2D plane.
Its goal is too find a line which separates the two groups of points into their respective colours.

This can be done with a purely deterministic greedy algorithm.
However, that takes too long and teaches me nothing about ML.
Instead i will build a linear classifier, using a purpose-built perceptron

In general, the idea is to generate a model and let the perceptron adjust its weights.
Afterwards, the user can test the machine for a given point and get his assumption of what color the point is.
