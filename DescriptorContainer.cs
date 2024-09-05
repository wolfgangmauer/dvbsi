using System;
using System.Collections.Generic;

namespace dvbsi
{
	public enum DescriptorScope 
	{
		ScopeSi,
		ScopeCarousel,
		ScopeMhp
	}

	public class DescriptorContainer : IDisposable
	{
		public List<Descriptor> Descriptors { get; private set; }

		public DescriptorContainer()
		{
			Descriptors = new List<Descriptor> ();
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			// free managed resources
			if (disposing) {
				foreach (var d in Descriptors) {
					d.Dispose ();
				}
				Descriptors.Clear ();
				Descriptors = null;
			}
			// free native resources if there are any.
		}

		protected void Descriptor (IReadOnlyList<byte> buffer, int index, DescriptorScope scope, bool back = true)
		{
			Descriptor d;
			switch (scope) 
			{
			case DescriptorScope.ScopeSi:
				d = DescriptorSi(buffer, index);
				break;
			case DescriptorScope.ScopeCarousel:
				d = DescriptorCarousel(buffer, index);
				break;
			case DescriptorScope.ScopeMhp:
				d = DescriptorMhp(buffer, index);
				break;
				default:
				/* ignore invalid scope */
				return;
			}
			if (d == null)
				return;
			if (!d.Valid)
				d.Dispose();
			else if (back)
				Descriptors.Add(d);
			else
				Descriptors.Insert(0, d);		
		}

	    private Descriptor DescriptorSi(IReadOnlyList<byte> buffer, int index)
		{
			var serviceTag = (SiDescriptorTag)buffer [index + 0];
			switch (serviceTag) {
			case SiDescriptorTag.VIDEO_STREAM_DESCRIPTOR:
				return new VideoStreamDescriptor (buffer, index);
			case SiDescriptorTag.AUDIO_STREAM_DESCRIPTOR:
				return new AudioStreamDescriptor (buffer, index);
//			case .REGISTRATION_DESCRIPTOR:
//				return new RegistrationDescriptor (buffer);
//			case SiDescriptorTag.TARGET_BACKGROUND_GRID_DESCRIPTOR:
//				return new TargetBackgroundGridDescriptor (buffer);
//			case SiDescriptorTag.VIDEO_WINDOW_DESCRIPTOR:
//				return new VideoWindowDescriptor (buffer);
			case SiDescriptorTag.CA_DESCRIPTOR:
				return new CaDescriptor (buffer, index);
			case SiDescriptorTag.LANGUAGE_DESCRIPTOR:
				return new LanguageDescriptor (buffer, index);
			case SiDescriptorTag.CAROUSEL_IDENTIFIER_DESCRIPTOR:
				return null; //new CarouselIdentifierDescriptor (buffer, index);
                             //			case SiDescriptorTag.NETWORK_NAME_DESCRIPTOR:
                             //				return new NetworkNameDescriptor (buffer);
                             //			case SiDescriptorTag.SERVICE_LIST_DESCRIPTOR:
                             //				return new ServiceListDescriptor (buffer);
                             //			case SiDescriptorTag.STUFFING_DESCRIPTOR:
                             //				return new StuffingDescriptor (buffer);
                             //			case SiDescriptorTag.SATELLITE_DELIVERY_SYSTEM_DESCRIPTOR:
                             //				return new SatelliteDeliverySystemDescriptor (buffer);
                             //			case SiDescriptorTag.CABLE_DELIVERY_SYSTEM_DESCRIPTOR:
                             //				return new CableDeliverySystemDescriptor (buffer);
                             //			case SiDescriptorTag.VBI_DATA_DESCRIPTOR:
                             //				return new VbiDataDescriptor (buffer);
                             //			case SiDescriptorTag.VBI_TELETEXT_DESCRIPTOR:
                             //				return new VbiTeletextDescriptor (buffer);
//                case SiDescriptorTag.BOUQUET_NAME_DESCRIPTOR:
//				return new BouquetNameDescriptor (buffer, index);
			case SiDescriptorTag.SERVICE_DESCRIPTOR:
				return new ServiceDescriptor (buffer, index);
//			case SiDescriptorTag.COUNTRY_AVAILABILITY_DESCRIPTOR:
//				return new CountryAvailabilityDescriptor (buffer);
			case SiDescriptorTag.LINKAGE_DESCRIPTOR:
				return new LinkageDescriptor (buffer, index);
//			case SiDescriptorTag.NVOD_REFERENCE_DESCRIPTOR:
//				return new NvodReferenceDescriptor (buffer);
			case SiDescriptorTag.TIME_SHIFTED_SERVICE_DESCRIPTOR:
				return new TimeShiftedServiceDescriptor (buffer, index);
			case SiDescriptorTag.SHORT_EVENT_DESCRIPTOR:
				return new ShortEventDescriptor (buffer, index);
			case SiDescriptorTag.EXTENDED_EVENT_DESCRIPTOR:
				return new ExtendedEventDescriptor (buffer, index);
			case SiDescriptorTag.COMPONENT_DESCRIPTOR:
			        return new ComponentDescriptor (buffer, index);
//			case SiDescriptorTag.MOSAIC_DESCRIPTOR:
//				return new MosaicDescriptor (buffer);
//			case SiDescriptorTag.STREAM_IDENTIFIER_DESCRIPTOR:
//				return new StreamIdentifierDescriptor (buffer);
			case SiDescriptorTag.CA_IDENTIFIER_DESCRIPTOR:
				return new CaIdentifierDescriptor (buffer, index);
            case SiDescriptorTag.CONTENT_DESCRIPTOR:
				return new ContentDescriptor (buffer, index);
            case SiDescriptorTag.PARENTAL_RATING_DESCRIPTOR:
				return new ParentalRatingDescriptor (buffer, index);
                             //			case SiDescriptorTag.TELETEXT_DESCRIPTOR:
                             //				return new TeletextDescriptor (buffer);
                             //			case SiDescriptorTag.TELEPHONE_DESCRIPTOR:
                             //				return new TelephoneDescriptor (buffer);
                             //			case SiDescriptorTag.LOCAL_TIME_OFFSET_DESCRIPTOR:
                             //				return new LocalTimeOffsetDescriptor (buffer);
                             //			case SiDescriptorTag.SUBTITLING_DESCRIPTOR:
                             //				return new SubtitlingDescriptor (buffer);
                             //			case SiDescriptorTag.TERRESTRIAL_DELIVERY_SYSTEM_DESCRIPTOR:
                             //				return new TerrestrialDeliverySystemDescriptor (buffer);
                             //			case SiDescriptorTag.MULTILINGUAL_NETWORK_NAME_DESCRIPTOR:
                             //				return new MultilingualNetworkNameDescriptor (buffer);
                             //			case SiDescriptorTag.MULTILINGUAL_BOUQUET_NAME_DESCRIPTOR:
                             //				return new MultilingualBouquetNameDescriptor (buffer);
                             //			case SiDescriptorTag.MULTILINGUAL_SERVICE_NAME_DESCRIPTOR:
                             //				return new MultilingualServiceNameDescriptor (buffer);
                             //			case SiDescriptorTag.MULTILINGUAL_COMPONENT_DESCRIPTOR:
                             //				return new MultilingualComponentDescriptor (buffer);
			case SiDescriptorTag.PRIVATE_DATA_SPECIFIER_DESCRIPTOR:
				return new PrivateDataSpecifierDescriptor (buffer, index);
                             //			case SiDescriptorTag.SERVICE_MOVE_DESCRIPTOR:
                             //				return new ServiceMoveDescriptor (buffer);
                             //			case SiDescriptorTag.SHORT_SMOOTHING_BUFFER_DESCRIPTOR:
                             //				return new ShortSmoothingBufferDescriptor (buffer);
//			case SiDescriptorTag.FREQUENCY_LIST_DESCRIPTOR:
//            	return new FrequencyListDescriptor (buffer);
 			case SiDescriptorTag.DATA_BROADCAST_DESCRIPTOR:
 				return new DataBroadcastDescriptor (buffer, index);
                             //			case SiDescriptorTag.SCRAMBLING_DESCRIPTOR:
                             //				return new ScramblingDescriptor (buffer);
                             //			case SiDescriptorTag.TRANSPORT_STREAM_DESCRIPTOR:
                             //				return new TransportStreamDescriptor (buffer);
                             //			case SiDescriptorTag.DSNG_DESCRIPTOR:
                             //				return new DSNGDescriptor (buffer);
			case SiDescriptorTag.PDC_DESCRIPTOR:
				return new PdcDescriptor (buffer, index);
            case SiDescriptorTag.AC3_DESCRIPTOR:
				return null; //new Ac3Descriptor (buffer, index);
            case SiDescriptorTag.ANCILLARY_DATA_DESCRIPTOR:
				return new AncillaryDataDescriptor (buffer, index);
                             //			case SiDescriptorTag.CELL_LIST_DESCRIPTOR:
                             //				return new CellListDescriptor (buffer);
                             //			case SiDescriptorTag.CELL_FREQUENCY_LINK_DESCRIPTOR:
                             //				return new CellFrequencyLinkDescriptor (buffer);
			case SiDescriptorTag.ANNOUNCEMENT_SUPPORT_DESCRIPTOR:
				return new AnnouncementSupportDescriptor (buffer, index);
			case SiDescriptorTag.APPLICATION_SIGNALLING_DESCRIPTOR:
				return new ApplicationSignallingDescriptor (buffer, index);
                case SiDescriptorTag.ADAPTATION_FIELD_DATA_DESCRIPTOR:
				return null; //new AdaptationFieldDataDescriptor (buffer, index);
                             //			case SiDescriptorTag.SERVICE_IDENTIFIER_DESCRIPTOR:
                             //				return new ServiceIdentifierDescriptor (buffer);
                             //			case SiDescriptorTag.SERVICE_AVAILABILITY_DESCRIPTOR:
                             //				return new ServiceAvailabilityDescriptor (buffer);
                             //			case SiDescriptorTag.DEFAULT_AUTHORITY_DESCRIPTOR:
                             //				return new DefaultAuthorityDescriptor (buffer);
                             //			case SiDescriptorTag.RELATED_CONTENT_DESCRIPTOR:
                             //				return new RelatedContentDescriptor (buffer);
                             //			case SiDescriptorTag.TVA_ID_DESCRIPTOR:
                             //				return new TVAIdDescriptor (buffer);
                             //			case SiDescriptorTag.CONTENT_IDENTIFIER_DESCRIPTOR:
                             //				return new ContentIdentifierDescriptor (buffer);
                             //			case SiDescriptorTag.TIME_SLICE_FEC_IDENTIFIER_DESCRIPTOR:
                             //				return new TimeSliceFecIdentifierDescriptor (buffer);
                             //			case SiDescriptorTag.ECM_REPETITION_RATE_DESCRIPTOR:
                             //				return new ECMRepetitionRateDescriptor (buffer);
                             //			case SiDescriptorTag.S2_SATELLITE_DELIVERY_SYSTEM_DESCRIPTOR:
                             //				return new S2SatelliteDeliverySystemDescriptor (buffer);
                             //			case SiDescriptorTag.ENHANCED_AC3_DESCRIPTOR:
                             //				return new EnhancedAC3Descriptor (buffer);
                             //			case SiDescriptorTag.DTS_DESCRIPTOR:
                             //				return new DTSDescriptor (buffer);
                case SiDescriptorTag.AAC_DESCRIPTOR:
				return new AacDescriptor (buffer, index);
//			case SiDescriptorTag.XAIT_LOCATION_DESCRIPTOR:
//				return new XaitLocationDescriptor (buffer);
//			case SiDescriptorTag.FTA_CONTENT_MANAGEMENT_DESCRIPTOR:
//				return new FtaContentManagementDescriptor (buffer);
			case SiDescriptorTag.EXTENSION_DESCRIPTOR:
				return null; //DescriptorSiExtended (buffer, index);
                             //			case SiDescriptorTag.LOGICAL_CHANNEL_DESCRIPTOR:
                             //				return new LogicalChannelDescriptor (buffer);
                             //			case SiDescriptorTag.HD_SIMULCAST_LOGICAL_CHANNEL_DESCRIPTOR:
                             //				return new LogicalChannelDescriptor (buffer);
                default:
				return new Descriptor (buffer, index);
			}
		}

		//private Descriptor DescriptorSiExtended(IReadOnlyList<byte> buffer, int index)
		//{
		//	switch (buffer [index+2])
  //          {
		//	//case SiDescriptorTagExtension.IMAGE_ICON_DESCRIPTOR:
		//	//	return null; //new ImageIconDescriptor (buffer);
		//	//case SiDescriptorTagExtension.CPCM_DELIVERY_SIGNALLING_DESCRIPTOR:
		//	//	return null; //new CpcmDeliverySignallingDescriptor (buffer);
		//	//case SiDescriptorTagExtension.CP_DESCRIPTOR:
		//	//	return null; //new CpDescriptor (buffer);
		//	//case SiDescriptorTagExtension.CP_IDENTIFIER_DESCRIPTOR:
		//	//	return null; //new CpIdentifierDescriptor (buffer);
		//	default:
		//		return new ExtensionDescriptor (buffer,index);
		//	}		
		//}

		private Descriptor DescriptorCarousel(IReadOnlyList<byte> buffer, int index)
		{
			switch (buffer [index + 0]) {
////			case (byte)(byte)CarouselDescriptorTag.TYPE_DESCRIPTOR:
////				return new TypeDescriptor (buffer);

//			case (byte)CarouselDescriptorTag.NAME_DESCRIPTOR:
//				return new NameDescriptor (buffer, index);

////			case (byte)CarouselDescriptorTag.INFO_DESCRIPTOR:
////				return new InfoDescriptor (buffer);

//			case (byte)CarouselDescriptorTag.MODULE_LINK_DESCRIPTOR:
//				return new ModuleLinkDescriptor (buffer);

////			case (byte)CarouselDescriptorTag.CRC32_DESCRIPTOR:
////				return new Crc32Descriptor (buffer);

//			case (byte)CarouselDescriptorTag.LOCATION_DESCRIPTOR:
//				return new LocationDescriptor (buffer);

////			case (byte)CarouselDescriptorTag.EST_DOWNLOAD_TIME_DESCRIPTOR:
////				return new EstDownloadTimeDescriptor (buffer);
////
////			case (byte)CarouselDescriptorTag.GROUP_LINK_DESCRIPTOR:
////				return new GroupLinkDescriptor (buffer);
////
////			case (byte)CarouselDescriptorTag.COMPRESSED_MODULE_DESCRIPTOR:
////				return new CompressedModuleDescriptor (buffer);

//			case (byte)CarouselDescriptorTag.LABEL_DESCRIPTOR:
//				return new LabelDescriptor (buffer);

////			case (byte)CarouselDescriptorTag.CACHING_PRIORITY_DESCRIPTOR:
////				return new CachingPriorityDescriptor (buffer);
////
////			case (byte)CarouselDescriptorTag.CONTENT_TYPE_DESCRIPTOR:
////				return new ContentTypeDescriptor (buffer);
////
////            case SiDescriptorTag.PRIVATE_DATA_SPECIFIER_DESCRIPTOR:
////				return new PrivateDataSpecifierDescriptor (buffer);

			default:
				return new Descriptor (buffer, index);
			}
		}

	    private Descriptor DescriptorMhp(IReadOnlyList<byte> buffer, int index)
		{
			switch (buffer [index + 0]) {
//			case (byte)MhpDescriptorTag.APPLICATION_DESCRIPTOR:
//				return new ApplicationDescriptor (buffer);

//			case (byte)MhpDescriptorTag.APPLICATION_NAME_DESCRIPTOR:
//				return new ApplicationNameDescriptor (buffer);

////			case (byte)MhpDescriptorTag.TRANSPORT_PROTOCOL_DESCRIPTOR:
////				return new TransportProtocolDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.DVB_J_APPLICATION_DESCRIPTOR:
////				return new DvbJApplicationDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.DVB_J_APPLICATION_LOCATION_DESCRIPTOR:
////				return new DvbJApplicationLocationDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.EXTERNAL_APPLICATION_AUTHORISATION_DESCRIPTOR:
////				return new ExternalApplicationAuthorisationDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.DVB_HTML_APPLICATION_DESCRIPTOR:
////				return new DvbHtmlApplicationDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.DVB_HTML_APPLICATION_LOCATION_DESCRIPTOR:
////				return new DvbHtmlApplicationLocationDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.DVB_HTML_APPLICATION_BOUNDARY_DESCRIPTOR:
////				return new DvbHtmlApplicationBoundaryDescriptor (buffer);

//			case (byte)MhpDescriptorTag.APPLICATION_ICONS_DESCRIPTOR:
//				return new ApplicationIconsDescriptor (buffer);

//			case (byte)MhpDescriptorTag.PREFETCH_DESCRIPTOR:
//				return new PrefetchDescriptor (buffer);

////			case (byte)MhpDescriptorTag.DII_LOCATION_DESCRIPTOR:
////				return new DiiLocationDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.DELEGATED_APPLICATION_DESCRIPTOR:
////				return new DelegatedApplicationDescriptor (buffer);

//			case (byte)MhpDescriptorTag.PLUGIN_DESCRIPTOR:
//				return new PluginDescriptor (buffer);

//			case (byte)MhpDescriptorTag.APPLICATION_STORAGE_DESCRIPTOR:
//				return new ApplicationStorageDescriptor (buffer);

////			case (byte)MhpDescriptorTag.IP_SIGNALING_DESCRIPTOR:
////				return new IpSignalingDescriptor (buffer);
////
////			case SiDescriptorTag.PRIVATE_DATA_SPECIFIER_DESCRIPTOR:
////				return new PrivateDataSpecifierDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.GRAPHICS_CONSTRAINTS_DESCRIPTOR:
////				return new GraphicsConstraintsDescriptor (buffer);
////
////			case (byte)MhpDescriptorTag.SIMPLE_APPLICATION_LOCATION_DESCRIPTOR:
////				return new SimpleApplicationLocationDescriptor (buffer);

//			case (byte)MhpDescriptorTag.APPLICATION_USAGE_DESCRIPTOR:
//				return new ApplicationUsageDescriptor (buffer);

////			case (byte)MhpDescriptorTag.SIMPLE_APPLICATION_BOUNDARY_DESCRIPTOR:
////				return new SimpleApplicationBoundaryDescriptor (buffer);

			default:
				return new Descriptor (buffer, index);
			}
		}
	}
}

