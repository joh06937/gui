﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpintronicsGUI
{
	public class Configuration
	{
		public string defaultSaveDirectory = Directory.GetCurrentDirectory();
		public int tempFoldersToKeep = -1;
		public int[] sensorMultiplexerValues = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0, 17, 18, 19, 20, 21, 22, 23,
									24, 25, 26, 27, 28, 29, 30};
		public string bufferName = "PBS";
		public string mnpsName = "MACS";
		public int preloadBufferVolume = 10;
		public int defaultAddBufferVolume = 5;
		public int defaultAddMnpsVolume = 20;
		public string defaultVolumeUnit = "uL";
		public int wheatstoneAmplitude = 300;
		public string wheatstoneAmplitudeUnit = "mV";
		public int wheatstoneFrequency = 1000;
		public int coilAmplitude = 200;
		public string coilAmplitudeUnit = "mV";
		public int coilFrequency = 50;
		public int coilDcOffset = 0;
		public string coilDcOffsetUnit = "V";
		public int measurementPeriod = 1;

		public Configuration()
		{
			if(!File.Exists("./config.ini"))
			{
				FileStream configFile;
				configFile = File.Create("./config.ini");
				configFile.Close();
				configFile.Dispose();
				writeConfigFileValues();
			}

			this.defaultSaveDirectory = readStringConfiguration("DefaultSaveDirectory");
			this.sensorMultiplexerValues = readSensorMultiplexerValues();
			this.bufferName = readStringConfiguration("BufferName");
			this.mnpsName = readStringConfiguration("MnpsName");
			this.preloadBufferVolume = readIntConfiguration("PreloadBufferVolume");
			this.defaultAddBufferVolume = readIntConfiguration("DefaultAddBufferVolume");
			this.defaultAddMnpsVolume = readIntConfiguration("DefaultAddMnpsVolume");
			this.defaultVolumeUnit = readStringConfiguration("DefaultVolumeUnit");
			this.wheatstoneAmplitude = readIntConfiguration("WheatstoneAmplitude");
			this.wheatstoneAmplitudeUnit = readStringConfiguration("WheatstoneAmplitudeUnit");
			this.wheatstoneFrequency = readIntConfiguration("WheatstoneFrequency");
			this.coilAmplitude = readIntConfiguration("CoilAmplitude");
			this.coilAmplitudeUnit = readStringConfiguration("CoilAmplitudeUnit");
			this.coilFrequency = readIntConfiguration("CoilFrequency");
			this.coilDcOffset = readIntConfiguration("CoilDcOffset");
			this.coilDcOffsetUnit = readStringConfiguration("CoilDcOffsetUnit");
			this.measurementPeriod = readIntConfiguration("MeasurementPeriod");
		}

		private void writeConfigFileValues()
		{
			StreamWriter file = new StreamWriter("./config.ini");
			file.WriteLine("DefaultSaveDirectory:" + this.defaultSaveDirectory);
			file.WriteLine("TempFoldersToKeep:" + this.tempFoldersToKeep);
			file.Write("SensorMultiplexerValues:");
			for (int i = 0; i < sensorMultiplexerValues.Length; i++)
			{
				file.Write("-" + sensorMultiplexerValues[i]);
			}
			file.Write("-\n");
			file.WriteLine("BufferName:" + this.bufferName);
			file.WriteLine("MnpsName:" + this.mnpsName);
			file.WriteLine("PreloadBufferVolume:" + this.preloadBufferVolume);
			file.WriteLine("DefaultAddBufferVolume:" + this.defaultAddBufferVolume);
			file.WriteLine("DefaultAddMnpsVolume:" + this.defaultAddMnpsVolume);
			file.WriteLine("DefaultVolumeUnit:" + this.defaultVolumeUnit);

			file.WriteLine("WheatstoneAmplitude:" + this.wheatstoneAmplitude);
			file.WriteLine("WheatstoneAmplitudeUnit:" + this.wheatstoneAmplitudeUnit);
			file.WriteLine("WheatstoneFrequency:" + this.wheatstoneFrequency);
			file.WriteLine("CoilAmplitude:" + this.coilAmplitude);
			file.WriteLine("CoilAmplitudeUnit:" + this.coilAmplitudeUnit);
			file.WriteLine("CoilFrequency:" + this.coilFrequency);
			file.WriteLine("CoilDcOffset:" + this.coilDcOffset);
			file.WriteLine("CoilDcOffsetUnit:" + this.coilDcOffsetUnit);
			file.WriteLine("MeasurementPeriod:" + this.measurementPeriod);
			file.Flush();
			file.Close();
			file.Dispose();
		}


		public void saveConfigurations()
		{
			FileStream configFile;
			configFile = File.Create("./config.ini");
			configFile.Close();
			configFile.Dispose();
			writeConfigFileValues();
		}


		public string readStringConfiguration(string label)
		{
			StreamReader file = new StreamReader("./config.ini");
			string line = file.ReadToEnd();
			file.Close();
			file.Dispose();
			label += ":";
			int start = line.IndexOf(label, 0);
			int end = line.IndexOf("\n", start);
			line = line.Substring(start + label.Length, end - start - label.Length - 1);
			return line;
		}

		public int readIntConfiguration(string label)
		{
			StreamReader file = new StreamReader("./config.ini");
			string line = file.ReadToEnd();
			file.Close();
			file.Dispose();
			label += ":";
			int start = line.IndexOf(label, 0);
			int end = line.IndexOf("\n", start);
			line = line.Substring(start + label.Length, end - start - label.Length - 1);
			return System.Convert.ToInt32(line);
		}

		public int[] readSensorMultiplexerValues()
		{
			StreamReader file = new StreamReader("./config.ini");
			string line = file.ReadToEnd();
			file.Close();
			file.Dispose();
			int start = line.IndexOf("SensorMultiplexerValues:", 0);
			int end = line.IndexOf("\n", start);
			line = line.Substring(start + 24, end - start - 24);
			int[] array = new int[sensorMultiplexerValues.Length];
			for (int i = 0; i < sensorMultiplexerValues.Length; i++)
			{
				int first = line.IndexOf("-", 0);
				int last = line.IndexOf("-", first + 1);
				array[i] = System.Convert.ToInt32(line.Substring(first + 1, last - first - 1));
				line = line.Remove(first, last - first);
			}
			return array;
		}


		public void setDefaultSaveDirectory(string directory)
		{
			this.defaultSaveDirectory = directory;
			saveConfigurations();
		}

		public void setTempFoldersToKeep(int number)
		{
			this.tempFoldersToKeep = number;
			saveConfigurations();
		}

		public void setSensorMultiplexerValues(int[] array)
		{
			this.sensorMultiplexerValues = array;
			saveConfigurations();
		}

		public void setBufferName(string name)
		{
			this.bufferName = name;
			saveConfigurations();
		}

		public void setMnpsName(string name)
		{
			this.mnpsName = name;
			saveConfigurations();
		}

		public void setPreloadBufferVolume(int volume)
		{
			this.preloadBufferVolume = volume;
			saveConfigurations();
		}

		public void setDefaultAddBufferVolume(int volume)
		{
			this.defaultAddBufferVolume = volume;
			saveConfigurations();
		}

		public void setDefaultAddMnpsVolume(int volume)
		{
			this.defaultAddMnpsVolume = volume;
			saveConfigurations();
		}

		public void setDefaultVolumeUnit(string unit)
		{
			this.defaultVolumeUnit = unit;
			saveConfigurations();
		}

		public void setWheatstoneAmplitude(int amplitude)
		{
			this.wheatstoneAmplitude = amplitude;
			saveConfigurations();
		}

		public void setWheatstoneAmplitudeUnit(string unit)
		{
			this.wheatstoneAmplitudeUnit = unit;
			saveConfigurations();
		}

		public void setWheatstoneFrequency(int frequency)
		{
			this.wheatstoneFrequency = frequency;
			saveConfigurations();
		}

		public void setCoilAmplitude(int amplitude)
		{
			this.coilAmplitude = amplitude;
			saveConfigurations();
		}

		public void setCoilAmplitudeUnit(string unit)
		{
			this.coilAmplitudeUnit = unit;
			saveConfigurations();
		}

		public void setCoilFrequncy(int frequency)
		{
			this.coilFrequency = frequency;
			saveConfigurations();
		}

		public void setCoilDcOffset(int offset)
		{
			this.coilDcOffset = offset;
			saveConfigurations();
		}

		public void setCoilDcOffsetUnit(string unit)
		{
			this.coilDcOffsetUnit = unit;
			saveConfigurations();
		}

		public void setMeasurementPeriod(int period)
		{
			this.measurementPeriod = period;
			saveConfigurations();
		}
	}
}
