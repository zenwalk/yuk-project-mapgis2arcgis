using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace MapArcGIS
{
    public class WorkSpaceWT
    {
        internal class PointData
        {
            public int ID;
            public short stringSum;
            public Int32 stringOffset;
            public double positionX;
            public double positionY;
            public byte pointPype;
            public byte transpresent;
            public short layer;
            public Int32 color;
        }
        internal class StringPointData : PointData
        {
            //public PointData pointData;
            public string stringData;
            public float charHeight;
            public float charWidth;
            public float charSpace;
            public float stringAngle;
            public short chinaFont;
            public short englishFont;
            public byte fontShape;
            public byte arragement;

        }
        internal class SubMapPointData : PointData
        {
            //public PointData pointData;
            public Int32 subNumber;
            public float subHeight;
            public float subWidth;
            public float subAngle;
            public float lineWidth;
            public float assColor;
        }
        internal class CriclePointData : PointData
        {
            //public PointData pointData;
            public float radius;
            public Int32 lineColor;
            public float lineWidth;
            public byte sign;//0是空心圆，1不是
        }
        internal class ArcPointData : PointData
        {
            //public PointData pointData;
            public double radius;
            public float startAngle;
            public float endAngle;
            public float lineWidth;

        }
        internal class ImagePointData : PointData
        {
            //public PointData pointData;
            public string imgaeFileName;
            public float charHeight;
            public float charWidth;
            public float stringAngle;
        }
        internal class TextPointData : PointData
        {
            //public PointData pointData;
            public string textData;
            public float charHeight;
            public float charWidth;
            public float charSpace;
            public float stringAngle;
            public short chinaFont;
            public short englishFont;
            public byte fontShape;
            public float verticalSpacing;
            public float sheetHeight;
            public float sheetWidth;
            public float sheetSpace;
            public byte arragement;
        }
        internal List<PointData> pointDatas;

        internal List<byte[]> pointFeatures;
        
        public override void LoadData(WorkSpaceInfo wsi)
        {
            
            base.LoadData(wsi);
            loadData();
        }
        private void loadData()
        {
            int pointsDataLength = dataHeads[0].dataOffset + dataHeads[0].size;
            workSpaceFile.Seek(dataHeads[0].dataOffset + 93, SeekOrigin.Begin);
            pointDatas = new List<PointData>();
            int id = 1;//mapgis里的属性表ID是从1开始的，0位置存放的为空
            while (workSpaceFile.Position <= pointsDataLength)
            {
                //if (br.ReadByte() != 1)
                //{
                //    return;
                //}

                id++;
                long temp = workSpaceFile.Position;
 
                workSpaceFile.Seek(30, SeekOrigin.Current);
                byte pointPype = br.ReadByte();
                workSpaceFile.Seek(temp, SeekOrigin.Begin);
                long current;
                switch (pointPype)
                {
                    case 0:
                        StringPointData spd = new StringPointData();
                        spd.pointPype = pointPype;
                        spd.stringSum = br.ReadInt16();
                        spd.stringOffset = br.ReadInt32();
                        spd.positionX = br.ReadDouble();
                        spd.positionY = br.ReadDouble();
                        current = workSpaceFile.Position;
                        workSpaceFile.Seek(dataHeads[1].dataOffset + spd.stringOffset, SeekOrigin.Begin);
                        spd.stringData = System.Text.Encoding.Default.GetString(br.ReadBytes(spd.stringSum));
                        workSpaceFile.Seek(current, SeekOrigin.Begin);
                        spd.charHeight = br.ReadSingle();
                        spd.charWidth = br.ReadSingle();
                        spd.charSpace = br.ReadSingle();
                        spd.stringAngle = br.ReadSingle();
                        spd.chinaFont = br.ReadInt16();
                        spd.englishFont = br.ReadInt16();
                        spd.fontShape = br.ReadByte();
                        spd.arragement = br.ReadByte();
                        workSpaceFile.Seek(17, SeekOrigin.Current);
                        spd.transpresent = br.ReadByte();
                        spd.layer = br.ReadInt16();
                        spd.color = br.ReadInt32();
                        pointDatas.Add(spd);
                        workSpaceFile.Seek(temp, SeekOrigin.Begin);
                        workSpaceFile.Seek(93, SeekOrigin.Current);
                        continue;
                    case 1:
                        SubMapPointData smpd = new SubMapPointData();
                        //smpd = pointData;
                        smpd.pointPype = pointPype;
                        smpd.stringSum = br.ReadInt16();
                        smpd.stringOffset = br.ReadInt32();
                        smpd.positionX = br.ReadDouble();
                        smpd.positionY = br.ReadDouble();
                        smpd.subNumber = br.ReadInt32();
                        smpd.subHeight = br.ReadSingle();
                        smpd.subWidth = br.ReadSingle();
                        smpd.subAngle = br.ReadSingle();
                        smpd.lineWidth = br.ReadSingle();
                        smpd.assColor = br.ReadUInt32();
                        workSpaceFile.Seek(15, SeekOrigin.Current);
                        smpd.transpresent = br.ReadByte();
                        smpd.layer = br.ReadInt16();
                        smpd.color = br.ReadInt32();
                        pointDatas.Add(smpd);
                        workSpaceFile.Seek(14, SeekOrigin.Current);
                        continue;
                    case 2:
                        CriclePointData cpd = new CriclePointData();
                        cpd.pointPype = pointPype;
                        cpd.stringSum = br.ReadInt16();
                        cpd.stringOffset = br.ReadInt32();
                        cpd.positionX = br.ReadDouble();
                        cpd.positionY = br.ReadDouble();
                        cpd.radius = br.ReadSingle();
                        cpd.lineColor = br.ReadInt32();
                        cpd.lineWidth = br.ReadSingle();
                        cpd.sign = br.ReadByte();
                        workSpaceFile.Seek(24, SeekOrigin.Current);
                        cpd.transpresent = br.ReadByte();
                        cpd.layer = br.ReadInt16();
                        cpd.color = br.ReadInt32();
                        pointDatas.Add(cpd);
                        workSpaceFile.Seek(14, SeekOrigin.Current);
                        continue;
                    case 3:
                        ArcPointData apd = new ArcPointData();
                        apd.pointPype = pointPype;
                        apd.stringSum = br.ReadInt16();
                        apd.stringOffset = br.ReadInt32();
                        apd.positionX = br.ReadDouble();
                        apd.positionY = br.ReadDouble();
                        apd.radius = br.ReadDouble();
                        apd.startAngle = br.ReadSingle();
                        apd.endAngle = br.ReadSingle();
                        apd.lineWidth = br.ReadSingle();
                        workSpaceFile.Seek(21, SeekOrigin.Current);
                        apd.transpresent = br.ReadByte();
                        apd.layer = br.ReadInt16();
                        apd.color = br.ReadInt32();
                        pointDatas.Add(apd);
                        workSpaceFile.Seek(14, SeekOrigin.Current);
                        continue;
                    case 4:
                        ImagePointData ipd = new ImagePointData();
                        ipd.pointPype = pointPype;
                        ipd.stringSum = br.ReadInt16();
                        ipd.stringOffset = br.ReadInt32();
                        ipd.positionX = br.ReadDouble();
                        ipd.positionY = br.ReadDouble();
                        current = workSpaceFile.Position;
                        workSpaceFile.Seek(dataHeads[1].dataOffset + ipd.stringOffset - ipd.stringSum, SeekOrigin.Begin);
                        ipd.imgaeFileName = br.ReadString();
                        workSpaceFile.Seek(current, SeekOrigin.Begin);
                        ipd.charHeight = br.ReadSingle();
                        ipd.charWidth = br.ReadSingle();
                        ipd.stringAngle = br.ReadSingle();
                        workSpaceFile.Seek(29, SeekOrigin.Current);
                        ipd.transpresent = br.ReadByte();
                        ipd.layer = br.ReadInt16();
                        ipd.color = br.ReadInt32();
                        pointDatas.Add(ipd);
                        workSpaceFile.Seek(14, SeekOrigin.Current);

                        continue;
                    case 5:

                        TextPointData tpd = new TextPointData();
                        tpd.pointPype = pointPype;
                        tpd.stringSum = br.ReadInt16();
                        tpd.stringOffset = br.ReadInt32();
                        tpd.positionX = br.ReadDouble();
                        tpd.positionY = br.ReadDouble();
                        current = workSpaceFile.Position;
                        workSpaceFile.Seek(dataHeads[1].dataOffset + tpd.stringOffset - tpd.stringSum, SeekOrigin.Begin);
                        tpd.textData = br.ReadString();
                        workSpaceFile.Seek(current, SeekOrigin.Begin);
                        tpd.charHeight = br.ReadSingle();
                        tpd.charWidth = br.ReadSingle();
                        tpd.charSpace = br.ReadSingle();
                        tpd.stringAngle = br.ReadSingle();
                        tpd.chinaFont = br.ReadInt16();
                        tpd.englishFont = br.ReadInt16();
                        tpd.fontShape = br.ReadByte();
                        tpd.sheetSpace = br.ReadSingle();
                        tpd.sheetHeight = br.ReadSingle();
                        tpd.sheetWidth = br.ReadSingle();
                        tpd.arragement = br.ReadByte();
                        workSpaceFile.Seek(7, SeekOrigin.Current);
                        tpd.transpresent = br.ReadByte();
                        tpd.layer = br.ReadInt16();
                        tpd.color = br.ReadInt32();
                        pointDatas.Add(tpd);
                        workSpaceFile.Seek(14, SeekOrigin.Current);
                        continue;
                    default:
                        break;
                }
                pointFeatures = new List<byte[]>();
                workSpaceFile.Seek(dataHeads[2].dataOffset, SeekOrigin.Begin);
                byte[] pointFeature = br.ReadBytes(dataHeads[2].size);
                pointFeatures.Add(pointFeature);
                workSpaceFile.Seek(dataHeads[2].dataOffset+0x0c, SeekOrigin.Begin);
                int tableItemOffset = br.ReadInt32();
                
                
                //br.Close();
            }
            workSpaceFile.Seek(dataHeads[2].dataOffset + 0x142, SeekOrigin.Begin);//140是表头文件的开始偏移位置强两位是标示符
            table = new FeatureTable();
            table.tableHeadNumber = br.ReadInt16();
            table.tableItemNumer = br.ReadInt32();
            table.tableItemLengthInBytes = br.ReadInt32();
            workSpaceFile.Seek(16, SeekOrigin.Current);
            table.tableHead = new List<FeatureTableHead>();
            for (int i = 0; i < table.tableHeadNumber; i++)
            {
                long tmp = workSpaceFile.Position;
                FeatureTableHead tableHead = new FeatureTableHead();
                tableHead.headName = System.Text.Encoding.Default.GetString(br.ReadBytes(20)).Trim();//表单元名占20个字节
                tableHead.itemType = br.ReadByte();
                tableHead.offset = br.ReadInt32();
                tableHead.lengthInBytes = br.ReadInt16();
                tableHead.tableItemCharLength = br.ReadInt16();
                table.tableHead.Add(tableHead);
                workSpaceFile.Seek(10, SeekOrigin.Current);
            }
            workSpaceFile.Seek(table.tableItemLengthInBytes, SeekOrigin.Current);
            table.tableItem = new List<byte[]>();
            for (int i = 0; i < table.tableItemNumer; i++)
            {
                br.ReadByte();
                table.tableItem.Add(br.ReadBytes(table.tableItemLengthInBytes - 1));
            }
        }
        public void PrintFeatureTable()
        {
            FileStream fs = new FileStream(workInfo.fileName + ".table", FileMode.OpenOrCreate);
            BinaryWriter ws = new BinaryWriter(fs);

            foreach (var item in pointFeatures)
            {
                ws.Write(item);
            }
            ws.Close();
            //FileStream fs = new FileStream(workInfo.fileName + ".txt", FileMode.OpenOrCreate);
            //BinaryWriter ws = new BinaryWriter(fs);

            //foreach (var item in table.tableItem)
            //{
            //    ws.Write(item);
            //}
            //ws.Close();

        }
        public void LoadDataFromFile(string fileName)
        {
            base.LoadData(fileName);
            this.fileName = fileName;
            loadData();
        }
        public void ConvertToShapeFileAndIndexFile()
        {
            FileStream shapeFile = new FileStream(fileName+".shp",FileMode.OpenOrCreate);
            BinaryWriter shpWR = new BinaryWriter(shapeFile);
            shpWR.Write(170328064);
            for (int i = 0; i < 6; i++)
            {
                shpWR.Write(0);
            }
            shpWR.Write(1000);
            shpWR.Write(1);
            for (int i = 0; i < 4; i++)
            {
                shpWR.Write(this.aeraBound[i]);
            }
            for (int i = 0; i < 8; i++)
            {
                shpWR.Write(0);
            }
            for (int i = 1; i < this.pointsNumber; i++)
            {
                shpWR.Write(i);
                shpWR.Write(10);
                shpWR.Write(1);
                shpWR.Write(this.pointDatas[i - 1].positionX);
                shpWR.Write(this.pointDatas[i - 1].positionY);
            }
            shapeFile.Seek(24, SeekOrigin.Begin);
            shpWR.Write(ReverseArray((int)shapeFile.Length));
            shpWR.Close();
            shpWR.Dispose();
            shapeFile.Close();
            shapeFile.Dispose();
        }
        public void ConverToIndexFIle()
        {

        }
        public byte[] ReverseArray(int value)
        {
            byte[] temp, result;
            temp = BitConverter.GetBytes(Convert.ToDouble(value));
            result = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                result[i] = temp[temp.Length - i];
            }
            return result;
        }
        //public void 
    }
}
