#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class PATIMachineLearningInputsV2 : Indicator
	{
		
		// variables
		#region Variables

		// private Bollinger bb;
		//private NinjaTrader.NinjaScript.Indicators.futuresAnalytica.ReverseStrength rsTop;
		//private NinjaTrader.NinjaScript.Indicators.futuresAnalytica.ReverseStrength rsBottom;
		// private EMA maFast;
		// private EMA maSlow;
		private DateTime glLastVolPerSecondReading;
		
		#endregion
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"This Indicator makes Data Plots available to train Machine Learning models used by Polarity ATI ";
				Name										= "PATIMachineLearningInputsV2";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= false;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				// AddPlot(Brushes.Orange, "BollingerDiff");
				//AddPlot(Brushes.Blue, "ReverseStrengthLinesDiff");
				// AddPlot(Brushes.Crimson, "MovingAvgDiff");
				AddPlot(Brushes.Maroon, "VolumeSpeedPerSecond");
				// AddPlot(Brushes.Magenta, "StdDevBB");
				// AddPlot(Brushes.Transparent, "TimeOfDay");
				/* EXAMPLES
				//AddPlot(Brushes.AliceBlue, "RSquareVolSpeed");
				//AddPlot(Brushes.Aqua, "LinRegVolSpeed");
				//AddPlot(Brushes.Aquamarine, "LinRegInterceptVolSpeed");
				//AddPlot(Brushes.Azure, "LinRegSlopeVolSpeed");
				AddPlot(Brushes.Maroon, "AverageBidAskRatio");
				AddPlot(Brushes.Maroon, "AverageMinimumImbalanceVolume");
				*/
			/*}
			else if (State == State.Configure)
			{
			}
			
			else if (State == State.DataLoaded)
			{
				bb = Bollinger(2,14);
				//rsTop = ReverseStrength(Close, 14, 60);
				//rsBottom = ReverseStrength(Close, 14, 40);
				maFast = EMA(33);
				maSlow = EMA(89);
				// Initialize last vol per second time interval;
				glLastVolPerSecondReading = DateTime.MinValue;
			*/}
		}

		protected override void OnConnectionStatusUpdate(ConnectionStatusEventArgs connectionStatusUpdate)
		{
			
		}

		protected override void OnFundamentalData(FundamentalDataEventArgs fundamentalDataUpdate)
		{
			
		}

		/// <summary>
		/// Handles the event when market data updates.
		/// </summary>
		/// <param name="marketDataUpdate">The arguments associated with the market data update event.</param>
		protected override void OnMarketData(MarketDataEventArgs marketDataUpdate)
		{
		    
		}

		/// <summary>
		/// Handles the event when market depth updates.
		/// </summary>
		/// <param name="marketDepthUpdate">The arguments associated with the market depth update event.</param>
		protected override void OnMarketDepth(MarketDepthEventArgs marketDepthUpdate)
		{
			// Currently, this method does nothing when the market depth updates.
		}

		protected override void OnBarUpdate()
		{
			if (CurrentBar < 2)
				return;

			//
			// Plots for Machine Learning Data Collection
			//

			// Bollinger Band Diff
			// Values[0][0] = bb.Upper[0] - bb.Lower[0];

			// Moving average Diff
			// Values[1][0] = Math.Abs(maFast[0] - maSlow[0]);

			// Volume per Second
			if (glLastVolPerSecondReading == DateTime.MinValue)
				glLastVolPerSecondReading = Time[0];
			TimeSpan t = Time[0] - glLastVolPerSecondReading;
			if (t.TotalSeconds >= 1)
			{
				double vVol = Instrument.MasterInstrument.InstrumentType == InstrumentType.CryptoCurrency ? Core.Globals.ToCryptocurrencyVolume((long)Volume[0]) : Volume[0];
				double ceil = Math.Ceiling(vVol / t.TotalSeconds);
				Values[2][0] =  (ceil >= 1) ? ceil : 1;
				glLastVolPerSecondReading = Time[0]; // reset
				//Print("PATIMachineLearningInputs : vVol=" + vVol + ", ceil=" + ceil + ", Values[2][0]=" + Values[2][0] + ", glLastVolPerSecondReading=" + glLastVolPerSecondReading);
			}

			// Standard Deviation
			// Values[3][0] = StdDev(Values[0], 40)[0];
			
			// Time of Day
			// Values[4][0] = (double)ToTime(Time[0].ToUniversalTime());
			//Print("PATIMachineLearningInputs : UTC=" + Values[4][0] + ", ToTime(Time[0])=" + ToTime(Time[0]).ToString());

			// EXAMPLES
			//Values[5][0] = RSquared(Values[2], 100)[0];
			//Values[6][0] = LinReg(Values[2], 40)[0];
			//Values[7][0] = LinRegIntercept(Values[2], 40)[0];
			//Values[8][0] = LinRegSlope(Values[2], 40)[0];
			// if (ReverseStrength1[0] < ReverseStrength2[0])
			//		Values[9][0] = rsTop[0] - rsBottom[0];
		}

		#region Properties

		// [Browsable(false)]
		// [XmlIgnore]
		// public Series<double> BollingerDiff
		// {
		// 	get { return Values[0]; }
		// }
/*
		[Browsable(false)]
		[XmlIgnore]
		public Series<double> ReverseStrengthLinesDiff
		{
			get { return Values[1]; }
		}
*/

		// /[Browsable(false)]
		// [XmlIgnore]
		// public Series<double> MovingAvgDiff
		// {
		//	get { return Values[1]; }
		// }

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> VolumeSpeedPerSecond
		{
			get { return Values[2]; }
		}

		// [Browsable(false)]
		// [XmlIgnore]
		// public Series<double> StdDevBB
		// {
		//	get { return Values[3]; }
		// }

		// [Browsable(false)]
		// [XmlIgnore]
		// public Series<double> TimeOfDay
		// {
		//	get { return Values[4]; }
		// }

		/*  EXAMPLES
		[Browsable(false)]
		[XmlIgnore]
		public Series<double> RSquareVolSpeed
		{
			get { return Values[4]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> LinRegVolSpeed
		{
			get { return Values[5]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> LinRegInterceptVolSpeed
		{
			get { return Values[6]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> LinRegSlopeVolSpeed
		{
			get { return Values[7]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> AverageBidAskRatio
		{
			get { return Values[3]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> AverageMinimumImbalanceVolume
		{
			get { return Values[4]; }
		}
		*/
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private PATIMachineLearningInputsV2[] cachePATIMachineLearningInputsV2;
		public PATIMachineLearningInputsV2 PATIMachineLearningInputsV2()
		{
			return PATIMachineLearningInputsV2(Input);
		}

		public PATIMachineLearningInputsV2 PATIMachineLearningInputsV2(ISeries<double> input)
		{
			if (cachePATIMachineLearningInputsV2 != null)
				for (int idx = 0; idx < cachePATIMachineLearningInputsV2.Length; idx++)
					if (cachePATIMachineLearningInputsV2[idx] != null &&  cachePATIMachineLearningInputsV2[idx].EqualsInput(input))
						return cachePATIMachineLearningInputsV2[idx];
			return CacheIndicator<PATIMachineLearningInputsV2>(new PATIMachineLearningInputsV2(), input, ref cachePATIMachineLearningInputsV2);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.PATIMachineLearningInputsV2 PATIMachineLearningInputsV2()
		{
			return indicator.PATIMachineLearningInputsV2(Input);
		}

		public Indicators.PATIMachineLearningInputsV2 PATIMachineLearningInputsV2(ISeries<double> input )
		{
			return indicator.PATIMachineLearningInputsV2(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.PATIMachineLearningInputsV2 PATIMachineLearningInputsV2()
		{
			return indicator.PATIMachineLearningInputsV2(Input);
		}

		public Indicators.PATIMachineLearningInputsV2 PATIMachineLearningInputsV2(ISeries<double> input )
		{
			return indicator.PATIMachineLearningInputsV2(input);
		}
	}
}

#endregion
