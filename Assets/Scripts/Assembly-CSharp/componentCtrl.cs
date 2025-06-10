using System;
using System.Collections.Generic;
using UnityEngine;

public class componentCtrl : MonoBehaviour
{
    [Serializable]
    public class PrefabData
    {
        public GameObject prefab_;

        public Vector3 pos_ = Vector3.zero;

        public int type_;
    }

    private static componentCtrl instance_;

    [SerializeField]
public List<PrefabData> prefab_data_ = new List<PrefabData>();

    [SerializeField]
public Canvas canvas_;

    [SerializeField]
public GameObject root_;

    [SerializeField]
public GameObject system_;

    [SerializeField]
public GameObject adv_canvas_;

    [SerializeField]
public GameObject adv_body_;

    private List<GameObject> obj_list_ = new List<GameObject>();

    public static componentCtrl instance
    {
        get
        {
            return instance_;
        }
    }

    private void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this;
        }
    }

    public void init()
    {
    }

    public void load()
    {
        foreach (PrefabData item in prefab_data_)
        {
            GameObject gameObject = Instantiate(item.prefab_);
            if (item.type_ == 0)
            {
                gameObject.transform.SetParent(canvas_.transform);
            }
            else if (item.type_ == 1)
            {
                gameObject.transform.SetParent(root_.transform);
            }
            else if (item.type_ == 2)
            {
                gameObject.transform.SetParent(system_.transform);
            }
            else if (item.type_ == 3)
            {
                gameObject.transform.SetParent(adv_canvas_.transform);
            }
            else
            {
                gameObject.transform.SetParent(adv_body_.transform);
            }
            gameObject.transform.localPosition = item.pos_;
            gameObject.transform.localScale = Vector3.one;
            obj_list_.Add(gameObject);
        }
        TouchSystem.instance.touch_camera_list.Clear();
        TouchSystem.instance.touch_camera_list.Add(systemCtrl.instance.system_camera);
        TouchSystem.instance.touch_camera_list.Add(titleCtrlRoot.instance.title_camera);
        TouchSystem.instance.touch_camera_list.Add(systemCtrl.instance.front_camera);
        TouchSystem.instance.touch_camera_list.Add(advCtrl.instance.adv_camera);
        TouchSystem.instance.touch_camera_list.Add(recordListCtrl.instance.board_camera);
        TouchSystem.instance.touch_camera_list.Add(scienceInvestigationCtrl.instance.science_camera);
        TouchSystem.instance.touch_camera_list.Add(ConfrontWithMovie.instance.movie_camera);
        messageBoardCtrl.instance.init();
        selectPlateCtrl.instance.init();
        recordListCtrl.instance.init();
        picePlateCtrl.instance.init();
        itemPlateCtrl.instance.init();
        facePlateCtrl.instance.init();
        judgmentCtrl.instance.init();
        doorCtrl.instance.init();
        gameoverCtrl.instance.init();
        lifeGaugeCtrl.instance.init();
        tanteiMenu.instance.init();
        moveCtrl.instance.init();
        inspectCtrl.instance.init();
        keyGuideCtrl.instance.init();
        ScreenCtrl.instance.init();
        ConfrontWithMovie.instance.system_camera = systemCtrl.instance.system_camera;
        ConfrontWithMovie.instance.key_guide = keyGuideCtrl.instance;
        recordListCtrl.instance.detail_ctrl.movie_player = ConfrontWithMovie.instance.movie_controller.gameObject;
        MovieAccessor.Instance.image = ScreenCtrl.instance.movie_rendere;
        AnimationCameraManager.Instance.irregular_image = BackAnimation.instance.irregular_image;
        AnimationCameraManager.Instance.canvas_camera = BackAnimation.instance.canvas_camera;
        AnimationCameraManager.Instance.main_camera = advCtrl.instance.adv_camera;
        fadeCtrl.instance.fade_target.Clear();
        fadeCtrl.instance.fade_target.Add(fadeCtrl.instance.target_list[0]);
        fadeCtrl.instance.fade_target.Add(fadeCtrl.instance.target_list[1]);
        fadeCtrl.instance.fade_target.Add(AnimationCameraManager.Instance.fade_targets);
        AnimationSystem.Instance.CameraManager = AnimationCameraManager.Instance;
        AnimationSystem.Instance.instance_parent = AnimationCameraManager.Instance.instance_parent;
        AnimationSystem.Instance.setup();
        bgCtrl.instance.parts_sprite.Clear();
        bgCtrl.instance.parts_sprite.Add(AnimationCameraManager.Instance.parts_sprite[0]);
        bgCtrl.instance.parts_sprite.Add(AnimationCameraManager.Instance.parts_sprite[1]);
        bgCtrl.instance.parts_sprite.Add(AnimationCameraManager.Instance.parts_sprite[2]);
        bgCtrl.instance.ending_table = AnimationCameraManager.Instance.ending_table;
        bgCtrl.instance.parts_parent = AnimationCameraManager.Instance.parts_sprite[0].transform.parent.gameObject;
        PointMiniGame.instance.cursor_touch = MiniGameCursor.instance.touch_cursor;
        MiniGameCursor.instance.canvas = canvas_;
        DyingMessageMiniGame.instance.main_canvas = canvas_;
        DyingMessageUtil.instance.cursor = MiniGameCursor.instance;
        luminolMiniGame.instance.bg_data = bgCtrl.instance.bg_data;
        NoiseCtrl.instance.bg_body = bgCtrl.instance.gameObject.transform;
        GS1_sce4_opening.instance.front_body = AnimationCameraManager.Instance.demo;
        GS1_sce4_opening.instance.front_image = systemCtrl.instance.front_image;
        GS1_sce4_opening.instance.front_sprites[0].sprite_renderer_ = AnimationCameraManager.Instance.front_sprites[0];
        GS1_sce4_opening.instance.front_sprites[1].sprite_renderer_ = AnimationCameraManager.Instance.front_sprites[1];
    }

    public void end()
    {
        fadeCtrl.instance.fade_target.Clear();
        fadeCtrl.instance.fade_target.Add(fadeCtrl.instance.target_list[0]);
        fadeCtrl.instance.fade_target.Add(fadeCtrl.instance.target_list[1]);
        AnimationSystem.Instance.end();
        TouchSystem.instance.touch_camera_list.Clear();
        TouchSystem.instance.touch_camera_list.Add(systemCtrl.instance.system_camera);
        TouchSystem.instance.touch_camera_list.Add(titleCtrlRoot.instance.title_camera);
        TouchSystem.instance.touch_camera_list.Add(systemCtrl.instance.front_camera);
        messageBoardCtrl.instance.Terminate();
        selectPlateCtrl.instance.Terminate();
        for (int i = 0; i < obj_list_.Count; i++)
        {
            UnityEngine.Object.Destroy(obj_list_[i]);
            obj_list_[i] = null;
        }
        obj_list_.Clear();
    }
}
