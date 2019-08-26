using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK.Graphics.OpenGL;
using Img = System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace AKMapEditor.OtMapEditor
{
    public class Sprite
    {
        public UInt32 id;
        public UInt32 size;
        private int textureId;
        public byte[] dump;

        public Sprite()
        {
            textureId = 0;
            id = 0;
            size = 0;
            dump = null;
        }


        public int getTextureId()
        {
            if (textureId == 0)
            {
                textureId = GraphicManager.createGLTexture(this);
            }
            return textureId;
        }
    }


    public class GameSprite
    {
        public UInt16 id;
        public UInt16 minimapColor;

        public byte width;
        public byte height;
        public byte frames;
        public byte xdiv;
        public byte ydiv;
        public byte zdiv;
        public byte animationLength;
        public UInt32 numSprites;
        public List<Sprite> spriteList = new List<Sprite>();

        public UInt16 drawoffset_x;
        public UInt16 drawoffset_y;

        public int draw_height = 0;

        public int image_index;

        public int getHardwareID(int _x, int _y, int _frame, int _count, int _xdiv, int _ydiv, int _zdiv, int _timeframe) 
        {
	        int v;
	        if(_count >= 0 && height <= 1 && width <= 1) {
		        v = _count;
	        } else {
		        v = ((((((_timeframe)*ydiv+_ydiv)*xdiv+_xdiv)*frames+_frame)*height+_y)*width+_x);
	        }
            if (v >= numSprites)
            {
                if (numSprites == 1)
                {
			        v = 0;
		        } else {
                    v %= (int) numSprites;
		        }
	        }            
            return spriteList[v].getTextureId();
        }

        public int getHardwareID(int _x, int _y, int _dir, Outfit _outfit, int _timeframe)
        {
            int v = 0;
            v = ((((_dir) * frames) * height + _y) * width + _x);
            if (v >= numSprites)
            {
                if (numSprites == 1)                
                {
                    v = 0;
                }
                else
                {
                    v %= (int) numSprites;
                }
            }
        /*
            if (frames > 1) // Template
            {
                TemplateImage img = getTemplateImage(v, _outfit);
                return img.getHardwareID();
            } */
            return spriteList[v].getTextureId();
        }


        public int getSpriteID(int _x, int _y, int _dir, Outfit _outfit, int _timeframe)
        {
            int v = 0;
            v = ((((_dir) * frames) * height + _y) * width + _x);
            if (v >= numSprites)
            {
                if (numSprites == 1)
                {
                    v = 0;
                }
                else
                {
                    v %= (int)numSprites;
                }
            }
            /*
                if (frames > 1) // Template
                {
                    TemplateImage img = getTemplateImage(v, _outfit);
                    return img.getHardwareID();
                } */
            return v;
        }

        public int getSpriteID(int _x, int _y, int _frame, int _count, int _xdiv, int _ydiv, int _zdiv, int _timeframe)
        {
            int v;
            if (_count >= 0 && height <= 1 && width <= 1)
            {
                v = _count;
            }
            else
            {
                v = ((((((_timeframe) * ydiv + _ydiv) * xdiv + _xdiv) * frames + _frame) * height + _y) * width + _x);
            }
            if (v >= numSprites)
            {
                if (numSprites == 1)
                {
                    v = 0;
                }
                else
                {
                    v %= (int)numSprites;
                }
            }
            return v;
        }

        public int getDrawHeight()
        {
            return draw_height;
        }

        public Bitmap getCreatureBitmap()
        {
            byte TRANSPARENT_COLOR = 0x11;

            int Width = 32;
            int Height = 32;

            Bitmap canvas = new Bitmap(Width, Height, Img.PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(canvas);

            for (int cx = 0; cx != this.width; cx++)
            {
                for (int cy = 0; cy != this.height; cy++)
                {
                    int texnum = getSpriteID(cx, cy,
                            2,
                            null,
                            0
                        );

                    Bitmap bmp = GraphicManager.getBitmap(GraphicManager.GetRGBData(spriteList[texnum]),
                        Img.PixelFormat.Format24bppRgb, 32, 32);

                    bmp.MakeTransparent(Color.FromArgb(TRANSPARENT_COLOR,
                        TRANSPARENT_COLOR, TRANSPARENT_COLOR));

                    if (width > 1 && height > 1)
                    {

                        g.DrawImage(bmp, new Rectangle(
                            Math.Max(32 / this.width - cx * 32 / this.width, 0),
                            Math.Max(32 / this.height - cy * 32 / this.height, 0),
                            bmp.Width / this.width, bmp.Height / this.height));
                    }
                    else if (width > 1)
                    {
                        g.DrawImage(bmp, new Rectangle(
                                    Math.Max(32 / this.width - cx * 32 / this.width, 0),
                                    8,
                                    bmp.Width / this.width, bmp.Height / this.width));
                    }
                    else if (height > 1)
                    {

                        g.DrawImage(bmp, new Rectangle(
                            8,
                            Math.Max(32 / this.height - cy * 32 / this.height, 0),
                            bmp.Width / this.height, bmp.Height / this.height));
                    }
                    else
                    {
                        g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    }                    
                }
            }

            g.Save();
            return canvas;
        }

        public Bitmap getBitmap()
        {
            byte TRANSPARENT_COLOR = 0x11;

            int Width = 32;
            int Height = 32;

            int _xdiv = (1 % this.xdiv);
            int _ydiv = (1 % this.ydiv);
            int _zdiv = (7 % this.zdiv);

            Bitmap canvas = new Bitmap(Width, Height, Img.PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(canvas);
            
            for (int cx = 0; cx != this.width; cx++)
            {
                for (int cy = 0; cy != this.height; cy++)
                {
                    for (int cf = 0; cf != this.frames; cf++)
                    {
                        int texnum = getSpriteID(cx, cy, cf,
                                -1,
                                _xdiv,
                                _ydiv,
                                _zdiv,
                                0
                            );

                        Bitmap bmp = GraphicManager.getBitmap(GraphicManager.GetRGBData(spriteList[texnum]),
                            Img.PixelFormat.Format24bppRgb, 32, 32);

                        bmp.MakeTransparent(Color.FromArgb(TRANSPARENT_COLOR,
                            TRANSPARENT_COLOR, TRANSPARENT_COLOR));
                        
                        if (width > 1 && height > 1)
                        {
                            
                            g.DrawImage(bmp, new Rectangle(
                                Math.Max(32 / this.width - cx * 32 / this.width, 0),
                                Math.Max(32 / this.height - cy * 32 / this.height, 0),
                                bmp.Width / this.width, bmp.Height / this.height));
                        }
                        else if (width > 1)
                        {
                            g.DrawImage(bmp, new Rectangle(
                                        Math.Max(32 / this.width - cx * 32 / this.width, 0),
                                        8,
                                        bmp.Width / this.width, bmp.Height / this.width));
                        }
                        else if (height > 1)
                        {

                            g.DrawImage(bmp, new Rectangle(
                                8,
                                Math.Max(32 / this.height - cy * 32 / this.height, 0),
                                bmp.Width / this.height, bmp.Height / this.height));
                        }
                        else
                        {
                            g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                        } 
                    }
                }
            }

            g.Save();
            return canvas;
        }

    }

    
    public class GraphicManager
    {
        public const byte TRANSPARENT_COLOR = 0x11;

        public Dictionary<UInt32, GameSprite> spriteItems;
        public Dictionary<UInt32, Sprite> sprites;
        public ItemDatabase item_db;
        public ImageList imageItemList;
        public Dictionary<EditorSprite, Image> sprite_space;
        public UInt16 itemCount;
        public UInt16 creatureCount;
        public UInt16 effectCount;
        public UInt16 distanceCount;

        public GraphicManager(ItemDatabase item_db)
        {
            spriteItems = new Dictionary<uint, GameSprite>();
            sprites = new Dictionary<UInt32, Sprite>();
            sprite_space = new Dictionary<EditorSprite, Image>();
            this.item_db = item_db;

            imageItemList = new ImageList();
            imageItemList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imageItemList.ImageSize = new System.Drawing.Size(32, 32);
            imageItemList.TransparentColor = System.Drawing.Color.Transparent;


            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_SD_1x1] = loadPNGFile(PngFiles.rectangular_1_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_SD_3x3] = loadPNGFile(PngFiles.rectangular_2_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_SD_5x5] = loadPNGFile(PngFiles.rectangular_3_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_SD_7x7] = loadPNGFile(PngFiles.rectangular_4_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_SD_9x9] = loadPNGFile(PngFiles.rectangular_5_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_SD_15x15] = loadPNGFile(PngFiles.rectangular_6_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_SD_19x19] = loadPNGFile(PngFiles.rectangular_7_png);

            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_CD_1x1] = loadPNGFile(PngFiles.circular_1_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_CD_3x3] = loadPNGFile(PngFiles.circular_2_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_CD_5x5] = loadPNGFile(PngFiles.circular_3_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_CD_7x7] = loadPNGFile(PngFiles.circular_4_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_CD_9x9] = loadPNGFile(PngFiles.circular_5_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_CD_15x15] = loadPNGFile(PngFiles.circular_6_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_BRUSH_CD_19x19] = loadPNGFile(PngFiles.circular_7_png);


            sprite_space[EditorSprite.EDITOR_SPRITE_OPTIONAL_BORDER_TOOL] = loadPNGFile(PngFiles.optional_border_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_ERASER] = loadPNGFile(PngFiles.eraser_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_PZ_TOOL] = loadPNGFile(PngFiles.protection_zone_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_PVPZ_TOOL] = loadPNGFile(PngFiles.pvp_zone_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_NOLOG_TOOL] = loadPNGFile(PngFiles.no_logout_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_NOPVP_TOOL] = loadPNGFile(PngFiles.no_pvp_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_DOOR_NORMAL] = loadPNGFile(PngFiles.door_normal_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_DOOR_LOCKED] = loadPNGFile(PngFiles.door_locked_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_DOOR_MAGIC] = loadPNGFile(PngFiles.door_magic_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_DOOR_QUEST] = loadPNGFile(PngFiles.door_quest_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_WINDOW_NORMAL] = loadPNGFile(PngFiles.window_normal_png);
            sprite_space[EditorSprite.EDITOR_SPRITE_WINDOW_HATCH] = loadPNGFile(PngFiles.window_hatch_png);
        }


        public void LoadImgFiles(EditorSprite eSprite, Panel brushPanel)
        {
            Image ret = null;
            sprite_space.TryGetValue(eSprite, out ret);
            if (ret != null)
            {
                brushPanel.BackgroundImage = ret;
                //brushPanel.BackgroundImageLayout = ImageLayout.Center;
            }
            
        }


        public Image loadPNGFile(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(bytes, 0, bytes.Length);
            return Bitmap.FromStream(ms);
        }


        public GameSprite getCreatureSprite(int id)
        {
            if (id < 0) return null;
            return spriteItems[(uint) id + itemCount];
        }


        public int getImageIndex(UInt32 spriteId)
        {
            try
            {
                GameSprite sprite = spriteItems[spriteId];
                if (sprite != null)
                {
                    if (sprite.image_index == 0)
                    {
                        imageItemList.Images.AddStrip(sprite.getBitmap());

                        sprite.image_index = imageItemList.Images.Count - 1;
                    }

                    return sprite.image_index;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Item: " + spriteId + " message: \n" + e.Message);
                return 0;
            }
        }

        public int getCreatureImageIndex(int spriteId)
        {
            try
            {
                GameSprite sprite = getCreatureSprite(spriteId);
                if (sprite != null)
                {
                    if (sprite.image_index == 0)
                    {
                        imageItemList.Images.AddStrip(sprite.getCreatureBitmap());

                        sprite.image_index = imageItemList.Images.Count - 1;
                    }

                    return sprite.image_index;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Item creature: " + spriteId + " message: \n" + e.Message);
                return 0;
            }
        }

        public static int createGLTexture(Sprite sprite)
        {
            Bitmap bmp = getBitmap(GetRGBData(sprite),
                System.Drawing.Imaging.PixelFormat.Format24bppRgb, 32, 32);
            bmp.MakeTransparent(Color.FromArgb(TRANSPARENT_COLOR,
                TRANSPARENT_COLOR, TRANSPARENT_COLOR));

            return CreateSpriteTextureFromBitmap(bmp);
        }

        public static int CreateSpriteTextureFromBitmap(Bitmap bitmap)
        {
            int text = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, text);


            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);
            bitmap.Dispose();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            return (int) text;
        }

        public void loadSpriteMetadata(String fileName)
        {
            FileStream fileStream = new FileStream(@fileName, FileMode.Open);
            try
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    UInt32 datSignature = reader.ReadUInt32();

                    //get max id
                    itemCount        = reader.ReadUInt16();
                    creatureCount    = reader.ReadUInt16();
                    effectCount      = reader.ReadUInt16();
                    distanceCount    = reader.ReadUInt16();

                    UInt16 minclientID = 100; //items starts at 100
                    UInt32 maxclientID =  (UInt32) (itemCount + creatureCount);

                    UInt16 id = minclientID;
                    while (id <= maxclientID)
                    {
                        GameSprite item = new GameSprite();
                        item.id = id;
                        spriteItems[id] = item;
                        if (item_db.clientItems[item.id] != null)
                        {
                            item_db.clientItems[item.id].gameSprite = item;
                        }

                        // read the options until we find 0xff
                        byte optbyte;
                        do
                        {
                            optbyte = reader.ReadByte();
                            //Trace.WriteLine(String.Format("{0:X}", optbyte));

                            switch (optbyte)
                            {
                                case 0x00: //groundtile
                                    {
                                        reader.ReadUInt16();
                                        //item.groundSpeed = reader.ReadUInt16();
                                        //item.type = ItemType.Ground;
                                    } break;

                                case 0x01: //all OnTop
                                    {
                                      //  item.alwaysOnTop = true;
                                      //  item.alwaysOnTopOrder = 1;
                                    } break;

                                case 0x02: //can walk trough (open doors, arces, bug pen fence)
                                    {
                                       // item.alwaysOnTop = true;
                                        //item.alwaysOnTopOrder = 2;
                                    } break;

                                case 0x03: //can walk trough (arces)
                                    {
                                       // item.alwaysOnTop = true;
                                        //item.alwaysOnTopOrder = 3;
                                    } break;

                                case 0x04: //container
                                    {
                                      //  item.type = ItemType.Container;
                                    } break;

                                case 0x05: //stackable
                                    {
                                      //  item.isStackable = true;
                                        break;
                                    }

                                case 0x06: //unknown
                                    {
                                    } break;

                                case 0x07: //useable
                                    {
                                     //   item.hasUseWith = true;
                                    } break;

                                case 0x08: //read/write-able
                                    {
                                      //  item.isReadable = true;
                                        //item.isWriteable = true;
                                       // item.maxReadWriteChars = reader.ReadUInt16();
                                        reader.ReadUInt16();
                                    } break;

                                case 0x09: //readable
                                    {
                                       //item.isReadable = true;
                                       //item.maxReadChars = reader.ReadUInt16();
                                        reader.ReadUInt16();
                                    } break;

                                case 0x0A: //fluid containers
                                    {
                                       // item.type = ItemType.Fluid;
                                    } break;

                                case 0x0B: //splashes
                                    {
                                      //  item.type = ItemType.Splash;
                                    } break;

                                case 0x0C: //blocks solid objects (creatures, walls etc)
                                    {
                                       // item.blockObject = true;
                                    } break;

                                case 0x0D: //not moveable
                                    {
                                       // item.isMoveable = false;
                                    } break;

                                case 0x0E: //blocks missiles (walls, magic wall etc)
                                    {
                                        //item.blockProjectile = true;
                                    } break;

                                case 0x0F: //blocks pathfind algorithms (monsters)
                                    {
                                       // item.blockPathFind = true;
                                    } break;

                                case 0x10: //blocks monster movement (flowers, parcels etc)
                                    {
                                      //  item.isPickupable = true;
                                    } break;

                                case 0x11: //hangable objects (wallpaper etc)
                                    {
                                      //  item.isHangable = true;
                                    } break;

                                case 0x12: //horizontal wall
                                    {
                                      //  item.isHorizontal = true;
                                    } break;

                                case 0x13: //vertical wall
                                    {
                                     //   item.isVertical = true;
                                    } break;

                                case 0x14: //rotatable
                                    {
                                     //   item.isRotatable = true;
                                    } break;

                                case 0x15: //light info
                                    {
                                      //  item.lightLevel = reader.ReadUInt16();
                                      //  item.lightColor = reader.ReadUInt16();
                                        reader.ReadUInt16();
                                        reader.ReadUInt16();
                                    } break;

                                case 0x16: //unknown
                                    {
                                    } break;

                                case 0x17: //changes floor
                                    {
                                    } break;

                                case 0x18: //Draw offset
                                    { 
                                        item.drawoffset_x = reader.ReadUInt16();                                        
                                        item.drawoffset_y = reader.ReadUInt16();
                                    } break;

                                case 0x19:
                                    {
                                        //item.hasHeight = true;
                                        //UInt16 height = reader.ReadUInt16();
                                        item.draw_height = reader.ReadUInt16();
                                    } break;

                                case 0x1A: //unknown
                                    {                                       
                                    } break;


                                case 0x1B: //unknown
                                    {
                                    } break;

                                case 0x1C: //minimap color
                                    {
                                        item.minimapColor = reader.ReadUInt16();
                                        break;
                                    }

                                case 0x1D: //in-game help info
                                    {
                                        reader.ReadUInt16();
                                        //UInt16 opt = reader.ReadUInt16();
                                        //if (opt == 1112)
                                        //{
                                            //item.isReadable = true;
                                        //}
                                    } break;

                                case 0x1E: //full tile
                                    {
                                       // item.walkStack = true;
                                    } break;

                                case 0x1F: //look through (borders)
                                    {
                                      //  item.lookThrough = true;
                                    } break;

                                case 0x20: //unknown
                                    {
                                        reader.ReadUInt16();
                                    } break;

                                case 0x21: //market
                                    {
                                        reader.ReadUInt16(); // category
                                      //  item.wareId = reader.ReadUInt16(); // trade as
                                        reader.ReadUInt16(); // trade as
                                        reader.ReadUInt16(); // show as
                                        var size = reader.ReadUInt16();
                                        new string(reader.ReadChars(size));
                                        //item.name = new string(reader.ReadChars(size));

                                        reader.ReadUInt16(); // profession
                                        reader.ReadUInt16(); // level
                                    }
                                    break;

                                case 0xFF: //end of attributes
                                    {
                                    } break;
                                case 0x9C:
                                    {
                                        
                                    }break;
                                case 0x88:
                                    {
                                    }break;
                                case 0x89:
                                    {
                                    }break;
                                    /*default:
                                    {
                                        null = null;
                                     //   throw new Exception(String.Format("Plugin: Error while parsing, unknown optbyte 0x{0:X} at id {1}", optbyte, id)); 
                                    }*/
                            }
                        } while (optbyte != 0xFF);

                        item.width = reader.ReadByte();
                        item.height = reader.ReadByte();
                        if ((item.width > 1) || (item.height > 1))
                        {
                            reader.BaseStream.Position++;
                        }

                        item.frames = reader.ReadByte();
                        item.xdiv = reader.ReadByte();
                        item.ydiv = reader.ReadByte();
                        item.zdiv = reader.ReadByte();
                        item.animationLength = reader.ReadByte();
                        //item.isAnimation = item.animationLength > 1;

                        item.numSprites =
                            (UInt32)item.width * (UInt32)item.height *
                            (UInt32)item.frames *
                            (UInt32)item.xdiv * (UInt32)item.ydiv * item.zdiv *
                            (UInt32)item.animationLength;

                        // Read the sprite ids
                        for (UInt32 i = 0; i < item.numSprites; ++i)
                        {
                            var spriteId = reader.ReadUInt32();
                            Sprite sprite;
                            if (!sprites.TryGetValue(spriteId, out sprite))
                            {
                                sprite = new Sprite();
                                sprite.id = spriteId;
                                sprites[spriteId] = sprite;
                            }
                            item.spriteList.Add(sprite);
                        }                        
                        ++id;
                    }
                }
            }
            finally
            {
                fileStream.Close();
            }
        }
        public void loadSpriteData(String fileName)
        {
            FileStream fileStream = new FileStream(@fileName, FileMode.Open);
            try
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    UInt32 sprSignature = reader.ReadUInt32();

                    UInt32 totalPics = reader.ReadUInt32();

                    List<UInt32> spriteIndexes = new List<UInt32>();
                    for (uint i = 0; i < totalPics; ++i)
                    {
                        UInt32 index = reader.ReadUInt32();
                        spriteIndexes.Add(index);
                    }

                    UInt32 id = 1;
                    foreach (UInt32 element in spriteIndexes)
                    {
                        UInt32 index = element + 3;
                        reader.BaseStream.Seek(index, SeekOrigin.Begin);
                        UInt16 size = reader.ReadUInt16();

                        Sprite sprite;
                        if (sprites.TryGetValue(id, out sprite))
                        {
                            if (sprite != null && size > 0)
                            {
                                if (sprite.size > 0)
                                {
                                    //generate warning
                                }
                                else
                                {
                                    sprite.id = id;
                                    sprite.size = size;
                                    sprite.dump = reader.ReadBytes(size);

                                    sprites[id] = sprite;
                                }
                            }
                        }
                        else
                        {
                            reader.BaseStream.Seek(size, SeekOrigin.Current);
                        }
                        ++id;
                    }
                }
            }
            finally
            {
                fileStream.Close();
            }

        }

        public static Bitmap getBitmap(byte[] rgbData, Img.PixelFormat pixelFormat, int Width, int Height)
        {
            int bitPerPixel = Image.GetPixelFormatSize(pixelFormat);
            Bitmap bmp = new Bitmap(Width, Height, Img.PixelFormat.Format24bppRgb);
            Img.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                Img.ImageLockMode.ReadWrite, bmp.PixelFormat);

            if (pixelFormat == Img.PixelFormat.Format24bppRgb)
            {
                //reverse rgb
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                    {
                        byte r = rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 0];
                        byte g = rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 1];
                        byte b = rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 2];

                        rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 0] = b;
                        rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 1] = g;
                        rgbData[Width * (bitPerPixel / 8) * y + x * (bitPerPixel / 8) + 2] = r;
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbData, 0, bmpData.Scan0, rgbData.Length);
            }
            else
            {
                byte[] grayscale = new byte[Width * Height * 3];
                int n = 0;
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                    {
                        grayscale[n * 3 + 0] = rgbData[n];
                        grayscale[n * 3 + 1] = rgbData[n];
                        grayscale[n * 3 + 2] = rgbData[n];
                        ++n;
                    }
                }

                //bmpData.Stride = -bmpData.Stride;
                System.Runtime.InteropServices.Marshal.Copy(grayscale, 0, bmpData.Scan0, grayscale.Length);
            }


            bmp.UnlockBits(bmpData);
            return bmp;
        }

        public static byte[] GetRGBData(Sprite sprite)
        {

            byte[] rgb32x32x3 = new byte[32 * 32 * 3];
            UInt32 bytes = 0;
            UInt32 x = 0;
            UInt32 y = 0;
            Int32 chunkSize;

            while (bytes < sprite.size)
            {
                chunkSize = sprite.dump[bytes] | sprite.dump[bytes + 1] << 8;
                bytes += 2;


                for (int i = 0; i < chunkSize; ++i)
                {
                    // Transparent pixel
                    rgb32x32x3[96 * y + x * 3 + 0] = TRANSPARENT_COLOR;
                    rgb32x32x3[96 * y + x * 3 + 1] = TRANSPARENT_COLOR;
                    rgb32x32x3[96 * y + x * 3 + 2] = TRANSPARENT_COLOR;
                    x++;
                    if (x >= 32)
                    {
                        x = 0;
                        ++y;
                    }
                }

                if (bytes >= sprite.size) break; // We're done
                // Now comes a pixel chunk, read it!
                chunkSize = sprite.dump[bytes] | sprite.dump[bytes + 1] << 8;
                bytes += 2;
                for (int i = 0; i < chunkSize; ++i)
                {
                    byte red = sprite.dump[bytes + 0];
                    byte green = sprite.dump[bytes + 1];
                    byte blue = sprite.dump[bytes + 2];
                    rgb32x32x3[96 * y + x * 3 + 0] = red;
                    rgb32x32x3[96 * y + x * 3 + 1] = green;
                    rgb32x32x3[96 * y + x * 3 + 2] = blue;

                    bytes += 3;

                    x++;
                    if (x >= 32)
                    {
                        x = 0;
                        ++y;
                    }
                }
            }

            // Fill up any trailing pixels
            while (y < 32 && x < 32)
            {
                rgb32x32x3[96 * y + x * 3 + 0] = TRANSPARENT_COLOR;
                rgb32x32x3[96 * y + x * 3 + 1] = TRANSPARENT_COLOR;
                rgb32x32x3[96 * y + x * 3 + 2] = TRANSPARENT_COLOR;
                x++;
                if (x >= 32)
                {
                    x = 0;
                    ++y;
                }
            }

            return rgb32x32x3;
        }
    }
}
