﻿using System.Collections;
using System.Collections.Generic;
using DCL;
using DCL.Builder;
using NUnit.Framework;
using UnityEngine;

public class BIWModeControllerShould : IntegrationTestSuite_Legacy
{
    private BIWModeController biwModeController;
    private IContext context;

    protected override IEnumerator SetUp()
    {
        yield return base.SetUp();

        biwModeController = new BIWModeController();

        BIWActionController actionController = new BIWActionController();

        context = BIWTestUtils.CreateContextWithGenericMocks(
            actionController,
            biwModeController,
            SceneReferences.i
        );

        biwModeController.Initialize(context);
        actionController.Initialize(context);

        biwModeController.EnterEditMode(scene);
        actionController.EnterEditMode(scene);
    }

    [Test]
    public void SetFirstPersonMode()
    {
        //Arrange
        biwModeController.SetBuildMode(IBIWModeController.EditModeState.Inactive);

        //Act
        biwModeController.SetBuildMode(IBIWModeController.EditModeState.FirstPerson);

        //Assert
        Assert.IsTrue(biwModeController.GetCurrentStateMode() == IBIWModeController.EditModeState.FirstPerson);
        Assert.IsTrue(biwModeController.GetCurrentMode().GetType() == typeof(BIWFirstPersonMode));
    }

    [Test]
    public void SetGodMode()
    {
        //Arrange
        biwModeController.SetBuildMode(IBIWModeController.EditModeState.Inactive);

        //Act
        biwModeController.SetBuildMode(IBIWModeController.EditModeState.GodMode);

        //Assert
        Assert.IsTrue(biwModeController.GetCurrentStateMode() == IBIWModeController.EditModeState.GodMode);
        Assert.IsTrue(biwModeController.GetCurrentMode().GetType() == typeof(BIWGodMode));
    }

    [Test]
    public void InactiveMode()
    {
        //Arrange
        biwModeController.SetBuildMode(IBIWModeController.EditModeState.GodMode);

        //Act
        biwModeController.SetBuildMode(IBIWModeController.EditModeState.Inactive);

        //Assert
        Assert.IsTrue(biwModeController.GetCurrentStateMode() == IBIWModeController.EditModeState.Inactive);
        Assert.IsTrue(biwModeController.GetCurrentMode() == null);
    }

    protected override IEnumerator TearDown()
    {
        context.Dispose();
        yield return base.TearDown();
    }
}