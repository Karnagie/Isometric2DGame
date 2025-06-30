# Isometric2DGame

A comprehensive Unity 2D isometric game demonstrating advanced technical implementation, flexible architecture, and professional development practices within constrained timeframes.

## 🎯 Project Overview

This project was developed as a technical assessment featuring a 2D isometric perspective game built in Unity. Despite time constraints (development limited to weekends due to full-time employment), the implementation showcases high-quality architecture, custom systems, and professional development practices. The focus was on delivering a solid core implementation with extensible, maintainable code architecture.

## 📋 Table of Contents

- [Key Technical Achievements](#-key-technical-achievements)
- [Project Context](#-project-context)
- [Core Features](#-core-features)
- [Technical Implementation](#-technical-implementation)
- [Architecture Overview](#️-architecture-overview)
- [Gameplay](#-gameplay)

## 🏆 Key Technical Achievements

- **🏗️ ECS Architecture**: Flexible Entity Component System for scalable entity management and testing
- **🧠 Utility AI System**: Advanced AI implementation using utility-based decision making patterns
- **📝 Custom Logging System**: Comprehensive logging infrastructure with categorization and filtering
- **🎯 Custom Gizmos**: Visual debugging tools for HP visualization and raycasting systems
- **🔄 Game State Machine**: Robust state management with clean transitions and extensible architecture
- **🧪 Unit Test Infrastructure**: Comprehensive testing framework for core systems validation
- **🏛️ Flexible Architecture**: Modular, extensible design following SOLID principles
- **⚡ Performance Optimization**: Efficient systems designed for scalability and maintainability

## 📅 Project Context

**Development Time**: 9 hours  
**Primary Constraints**: Limited to weekend work due to full-time employment commitments  
**Assessment Scope**: 4 core technical tasks with additional bonus objectives  
**Priority Framework**: Core functionality and architecture quality over feature completeness  

## ✨ Core Features

### Gameplay Systems
- **Character Movement**: Smooth character controller with camera following
- **Visual Effects**: Death animations
- **Abilities**: Use only by enemies, but works for all
- **Ai**: Ai with routing feature and player chasing

### Technical Systems
- **Entity Management**: ECS-based flexible entity creation and management
- **AI Behavior**: Utility AI system for dynamic decision making
- **Health System**: Component-based HP management with visual indicators
- **Input Handling**: Robust input system with keyboard and mouse support

## 🔧 Technical Implementation

### Core Systems

#### Entity Component System (ECS)
```csharp
// Example component structure
[Game] public class MaxHp : IComponent { public float Value; }
[Game] public class CurrentHp : IComponent { public float Value; }
[Game] public class Dead : IComponent { }
[Game] public class ProcessingDeath : IComponent { }

// System processing
public class FinalizeDeathSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public FinalizeDeathSystem(GameContext game)
    {
        _entities = game.GetGroup(GameMatcher
            .AllOf(
                GameMatcher.ProcessingDeath,
                GameMatcher.CooldownUp));
    }

    public void Execute()
    {
        foreach (var entity in _entities.GetEntities(_buffer))
        {
            entity.isDestructed = true;
        }
    }
}
```

#### Custom Logging System
- **Categorized Logging**: Separate channels for Core, Infrastructure, Meta, Ui
- **Filtering Options**: Runtime filtering by category and severity level
- **Performance Optimized**: Conditional compilation for release builds

#### Utility AI Implementation
- **Modular Considerations**: Pluggable utility functions for different behaviors
- **Dynamic Decision Making**: Real-time evaluation of action priorities

## 🏗️ Architecture Overview

### System Architecture
![image](https://github.com/user-attachments/assets/ab2998bd-4841-4a1d-8d5b-20075a824b0d)


### Design Patterns Implemented
- **Entity Component System**: Composition over inheritance for flexible entity design
- **State Machine**: Clean game state management with defined transitions
- **Factory Pattern**: Consistent entity and component creation
- **DI**: Zenject

### Custom Gizmos Implementation
- **HP Visualization**: Real-time health bar rendering in Scene view
- **Raycast Debugging**: Visual representation of AI line-of-sight calculations

## 🛠️ Development Approach

### Code Quality Standards
- **SOLID Principles**: Applied throughout core architecture
- **Unit Testing**: Critical systems covered with automated tests
- **Consistent Naming**: Established conventions followed project-wide
- **Refactoring Opportunities**: Identified and documented for future iterations

## 🎮 Gameplay

### Controls
- **Movement**: WASD or Arrow Keys
- **Attack**: Left Mouse Button

### Gameplay in Editor
https://github.com/user-attachments/assets/2932535f-3657-4d26-8bd8-f0e352d5ad29

**Technical Decision**: Prioritized solid core implementation over extensive feature breadth to demonstrate architectural competency and sustainable development practices.

---
