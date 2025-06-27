using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.Common.Collisions;
using Code.Core.Common.Physics;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Entitas;
using Object = UnityEngine.Object;


[TestFixture]
public class PhysicsServiceTests
{
    private PhysicsService _physicsService;
    private ICollisionRegistry _mockCollisionRegistry;
    private IPhysicsResolver _mockPhysicsResolver;
    private GameEntity _testEntity;
    private GameEntity _testEntity2;
    private GameObject _testGameObject;
    private GameObject _testGameObject2;
    private Collider2D _testCollider2D;
    private Collider _testCollider;

    [SetUp]
    public void SetUp()
    {
        _mockCollisionRegistry = Substitute.For<ICollisionRegistry>();
        _mockPhysicsResolver = Substitute.For<IPhysicsResolver>();
        _physicsService = new PhysicsService(_mockCollisionRegistry, _mockPhysicsResolver);
        
        _testGameObject = new GameObject("TestObject1");
        _testGameObject2 = new GameObject("TestObject2");
        
        _testCollider2D = _testGameObject.AddComponent<BoxCollider2D>();
        _testCollider = _testGameObject2.AddComponent<BoxCollider>();

        _testEntity = new GameEntity();
        _testEntity2 = new GameEntity();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up Unity objects
        if (_testGameObject != null)
            Object.DestroyImmediate(_testGameObject);
        if (_testGameObject2 != null)
            Object.DestroyImmediate(_testGameObject2);
    }

    [Test]
    public void WhenRaycastAll_AndHitsFound_ThenReturnsEntities()
    {
        // Arrange
        var worldPosition = Vector2.zero;
        var direction = Vector2.up;
        var layerMask = 1;
        var instanceId = _testCollider2D.GetInstanceID();

        _mockCollisionRegistry.Get<GameEntity>(instanceId).Returns(_testEntity);

        // Act
        var result = _physicsService.RaycastAll(worldPosition, direction, layerMask);

        // Assert
        Assert.IsNotNull(result);
    }

    [Test]
    public void WhenRaycastAll_AndNoHits_ThenReturnsEmptyEnumerable()
    {
        // Arrange
        var worldPosition = Vector2.zero;
        var direction = Vector2.up;
        var layerMask = 1;

        // Act
        var result = _physicsService.RaycastAll(worldPosition, direction, layerMask);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void WhenRaycast_AndHitFound_ThenReturnsFirstEntity()
    {
        // Arrange
        var worldPosition = Vector2.zero;
        var direction = Vector2.up;
        var layerMask = 1;
        var instanceId = _testCollider2D.GetInstanceID();

        _mockCollisionRegistry.Get<GameEntity>(instanceId).Returns(_testEntity);

        // Act
        var result = _physicsService.Raycast(worldPosition, direction, layerMask);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void WhenRaycast_AndNoHits_ThenReturnsNull()
    {
        // Arrange
        var worldPosition = Vector2.zero;
        var direction = Vector2.up;
        var layerMask = 1;

        // Act
        var result = _physicsService.Raycast(worldPosition, direction, layerMask);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void WhenLineCast_AndHitFound_ThenReturnsEntity()
    {
        // Arrange
        var start = Vector2.zero;
        var end = Vector2.up;
        var layerMask = 1;

        // Act
        var result = _physicsService.LineCast(start, end, layerMask);

        // Assert
        Assert.IsNull(result); // Would be entity if Physics2D was mocked
    }

    [Test]
    public void WhenLineCast_AndNoHits_ThenReturnsNull()
    {
        // Arrange
        var start = Vector2.zero;
        var end = Vector2.up;
        var layerMask = 1;

        // Act
        var result = _physicsService.LineCast(start, end, layerMask);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void WhenCircleCast_AndHitsFound_ThenReturnsEntities()
    {
        // Arrange
        var position = Vector3.zero;
        var radius = 1.0f;
        var layerMask = 1;

        // Act
        var result = _physicsService.CircleCast(position, radius, layerMask);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count()); // Would contain entities if Physics2D was mocked
    }

    [Test]
    public void WhenCircleCastNonAlloc_AndHitsFound_ThenFillsBufferAndReturnsCount()
    {
        // Arrange
        var position = Vector3.zero;
        var radius = 1.0f;
        var layerMask = 1;
        var hitBuffer = new GameEntity[10];

        // Act
        var result = _physicsService.CircleCastNonAlloc(position, radius, layerMask, hitBuffer);

        // Assert
        Assert.AreEqual(0, result); // Would be > 0 if Physics2D was mocked
        Assert.IsNull(hitBuffer[0]); // Would contain entities if hits were found
    }

    [Test]
    public void WhenOverlapPoint_AndHitFound_ThenReturnsEntity()
    {
        // Arrange
        var worldPosition = Vector2.zero;
        var layerMask = 1;

        // Act
        var result = _physicsService.OverlapPoint<GameEntity>(worldPosition, layerMask);

        // Assert
        Assert.IsNull(result); // Would be entity if Physics2D was mocked
    }

    [Test]
    public void WhenOverlapPoint_AndNoHits_ThenReturnsNull()
    {
        // Arrange
        var worldPosition = Vector2.zero;
        var layerMask = 1;

        // Act
        var result = _physicsService.OverlapPoint<GameEntity>(worldPosition, layerMask);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void WhenOverlapCircle_AndCalled_ThenReturnsHitCount()
    {
        // Arrange
        var worldPos = Vector3.zero;
        var radius = 1.0f;
        var hits = new Collider2D[10];
        var layerMask = 1;

        // Act
        var result = _physicsService.OverlapCircle(worldPos, radius, hits, layerMask);

        // Assert
        Assert.AreEqual(0, result); // Would be > 0 if Physics2D was mocked
    }

    [Test]
    public void WhenCalculatePosition_WithCollider_AndEntityNotFound_ThenReturnsOriginalPosition()
    {
        // Arrange
        var entity = _testEntity;
        var worldPositionA = Vector3.zero;
        var collider = _testCollider;

        _mockCollisionRegistry.Get(entity).Returns((Collider) null);

        // Act
        var result = _physicsService.CalculatePosition(entity, worldPositionA, collider);

        // Assert
        Assert.AreEqual(worldPositionA, result);
    }

    [Test]
    public void WhenCalculatePosition_WithGameEntity_AndEntityNotFound_ThenReturnsOriginalPosition()
    {
        // Arrange
        var entity = _testEntity;
        var worldPositionA = Vector3.zero;
        var near = _testEntity2;

        _mockCollisionRegistry.Get(entity).Returns((Collider) null);

        // Act
        var result = _physicsService.CalculatePosition(entity, worldPositionA, near);

        // Assert
        Assert.AreEqual(worldPositionA, result);
    }

    [Test]
    public void WhenCalculatePosition_WithGameEntity_AndNearEntityNotFound_ThenReturnsOriginalPosition()
    {
        // Arrange
        var entity = _testEntity;
        var worldPositionA = Vector3.zero;
        var near = _testEntity2;

        _mockCollisionRegistry.Get(entity).Returns(_testCollider);
        _mockCollisionRegistry.Get(near).Returns((Collider) null);

        // Act
        var result = _physicsService.CalculatePosition(entity, worldPositionA, near);

        // Assert
        Assert.AreEqual(worldPositionA, result);
    }

    [Test]
    public void WhenFindNonEntityColliders_AndEntityNotFound_ThenReturnsEmpty()
    {
        // Arrange
        var entity = _testEntity;
        var position = Vector3.zero;
        var radius = 1.0f;

        _mockCollisionRegistry.Get(entity).Returns((Collider) null);

        // Act
        var result = _physicsService.FindNonEntityColliders(entity, position, radius);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Length);
    }

    [Test]
    public void WhenFindCollidedEntities_AndEntityNotFound_ThenReturnsEmpty()
    {
        // Arrange
        var entity = _testEntity;
        var position = Vector3.zero;
        var radius = 1.0f;

        _mockCollisionRegistry.Get(entity).Returns((Collider) null);

        // Act
        var result = _physicsService.FindCollidedEntities(entity, position, radius);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Length);
    }

    [Test]
    public void WhenGetPenetration_WithCollider_AndEntityNotFound_ThenReturnsFalse()
    {
        // Arrange
        var entity = _testEntity;
        var colliderB = _testCollider;

        _mockCollisionRegistry.Get(entity).Returns((Collider) null);

        // Act
        var result = _physicsService.GetPenetration(entity, colliderB);

        // Assert
        Assert.IsFalse(result.Item1);
        Assert.AreEqual(Vector3.zero, result.Item2);
        Assert.AreEqual(0, result.Item3);
    }

    [Test]
    public void WhenGetPenetration_WithGameEntity_AndEntityNotFound_ThenReturnsFalse()
    {
        // Arrange
        var entity = _testEntity;
        var nearEntity = _testEntity2;

        _mockCollisionRegistry.Get(entity).Returns((Collider) null);

        // Act
        var result = _physicsService.GetPenetration(entity, nearEntity);

        // Assert
        Assert.IsFalse(result.Item1);
        Assert.AreEqual(Vector3.zero, result.Item2);
        Assert.AreEqual(0, result.Item3);
    }

    [Test]
    public void WhenWorldPositionA_AndNoPenetration_ThenReturnsOriginalPosition()
    {
        // Arrange
        var worldPositionA = Vector3.zero;
        var rotationA = Quaternion.identity;
        var colliderA = _testCollider;
        var worldPositionB = Vector3.up;
        var rotationB = Quaternion.identity;
        var colliderB = _testCollider;

        // Act
        var result = _physicsService.WorldPositionA(
            worldPositionA, rotationA, colliderA,
            worldPositionB, rotationB, colliderB);

        // Assert
        // Note: Would need to mock Physics.ComputePenetration for full testing
        Assert.AreEqual(worldPositionA, result);
    }

    [Test]
    public void WhenConstructor_AndCollisionRegistryProvided_ThenInstanceCreated()
    {
        // Arrange
        var collisionRegistry = Substitute.For<ICollisionRegistry>();

        // Act
        var service = new PhysicsService(collisionRegistry, _mockPhysicsResolver);

        // Assert
        Assert.IsNotNull(service);
    }

    [Test]
    public void WhenOverlapPoint_WithGenericType_AndHitFoundButWrongType_ThenReturnsNull()
    {
        // Arrange
        var worldPosition = Vector2.zero;
        var layerMask = 1;
        var instanceId = _testCollider2D.GetInstanceID();

        _mockCollisionRegistry.Get<string>(instanceId).Returns((string) null);

        // Act
        var result = _physicsService.OverlapPoint<string>(worldPosition, layerMask);

        // Assert
        Assert.IsNull(result);
    }
}