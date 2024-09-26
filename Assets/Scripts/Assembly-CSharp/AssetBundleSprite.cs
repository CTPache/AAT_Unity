using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssetBundleSprite
{
    public SpriteRenderer sprite_renderer_;

    public List<Sprite> sprite_data_ = new List<Sprite>();

    public AssetBundle asset_bundle_;

    public int sprite_no_;

    public string path_ = string.Empty;

    public string name_ = string.Empty;

    public bool active
    {
        get
        {
            return obj.activeSelf;
        }
        set
        {
            obj.SetActive(value);
        }
    }

    public GameObject obj
    {
        get
        {
            return sprite_renderer_.gameObject;
        }
    }

    public Transform transform
    {
        get
        {
            return sprite_renderer_.gameObject.transform;
        }
    }

    public Color color
    {
        get
        {
            return sprite_renderer_.color;
        }
        set
        {
            sprite_renderer_.color = value;
        }
    }

    public void load(string in_path, string in_name, bool is_common = false, bool force = false)
    {
        path_ = in_path;
        name_ = in_name;
        AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name, is_common, -1, force);
        sprite_data_.Clear();
        //Debug.Log("Loading Sprite" + in_path + "/" + in_name + " assets: " + string.Join(", ", assetBundle.GetAllAssetNames()));
        sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
        sprite_renderer_.sprite = sprite_data_[0];
    }

    public void spriteNo(int in_sprite_no)
    {
        if (in_sprite_no < sprite_data_.Count)
        {
            sprite_renderer_.sprite = sprite_data_[in_sprite_no];
            sprite_no_ = in_sprite_no;
        }
    }

    public void remove()
    {
        AssetBundleCtrl.instance.remove(path_, name_);
    }

    public void end()
    {
        sprite_data_.Clear();
        if (asset_bundle_ != null)
        {
            asset_bundle_.Unload(true);
        }
    }
}
