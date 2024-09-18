using Steamworks;
using UnityEngine;

[DisallowMultipleComponent]
public class SteamAuthSessionScript : SteamScriptBase
{
	public delegate void OnAuthSession(bool success);

	private static SteamAuthSessionScript s_instance_;

	private bool requesting_;

	private byte[] auth_session_;

	private uint ticket_size_;

	private HAuthTicket auth_ticket_;

	private OnAuthSession on_complete_auth_session_request_;

	private Callback<ValidateAuthTicketResponse_t> m_ValidateAuthTicketResponse;

	public static SteamAuthSessionScript Instance
	{
		get
		{
			if (s_instance_ == null)
			{
				s_instance_ = SteamScriptBase.AddSteamScriptComponent<SteamAuthSessionScript>();
			}
			return s_instance_;
		}
	}

	protected override Rect gui_layout_area
	{
		get
		{
			return new Rect(Screen.width - 600, 0f, 200f, Screen.height);
		}
	}

	protected override void AwakeSteam()
	{
		if (s_instance_ == null)
		{
			s_instance_ = this;
		}
		m_ValidateAuthTicketResponse = Callback<ValidateAuthTicketResponse_t>.Create(OnValidateAuthTicketResponse);
	}

	protected override void StartSteam()
	{
	}

	protected override void OnActive()
	{
		base.OnActive();
		if (s_instance_ == null)
		{
			s_instance_ = this;
		}
	}

	protected override void OnDeactive()
	{
		base.OnDeactive();
		if (!(s_instance_ != this))
		{
			s_instance_ = null;
		}
	}

	private void OnDisable()
	{
		EndAuthSession();
	}

	public override void Init()
	{
		auth_session_ = null;
		ticket_size_ = 0u;
		auth_ticket_ = HAuthTicket.Invalid;
	}

	public void StartAuthSession(OnAuthSession callback)
	{
		if (requesting_)
		{
			return;
		}
		requesting_ = true;
		auth_session_ = new byte[1024];
		auth_ticket_ = SteamUser.GetAuthSessionTicket(auth_session_, auth_session_.Length, out ticket_size_);
		if (auth_ticket_ != HAuthTicket.Invalid && ticket_size_ != 0)
		{
			EBeginAuthSessionResult eBeginAuthSessionResult = SteamUser.BeginAuthSession(auth_session_, (int)ticket_size_, SteamUser.GetSteamID());
			Debug.Log(string.Concat("SteamUser.BeginAuthSession(auth_session_, ", ticket_size_, ", ", SteamUser.GetSteamID(), ") - ", eBeginAuthSessionResult));
			if (eBeginAuthSessionResult == EBeginAuthSessionResult.k_EBeginAuthSessionResultOK)
			{
				on_complete_auth_session_request_ = callback;
			}
			else if (callback != null)
			{
				Debug.LogError(eBeginAuthSessionResult);
				callback(false);
				requesting_ = false;
			}
		}
	}

	public void EndAuthSession()
	{
		SteamUser.CancelAuthTicket(auth_ticket_);
		SteamUser.EndAuthSession(SteamUser.GetSteamID());
		auth_session_ = null;
		ticket_size_ = 0u;
		auth_ticket_ = HAuthTicket.Invalid;
	}

	private void OnValidateAuthTicketResponse(ValidateAuthTicketResponse_t pCallback)
	{
		Debug.Log(string.Concat("[", 143, " - ValidateAuthTicketResponse] - ", pCallback.m_SteamID, " -- ", pCallback.m_eAuthSessionResponse, " -- ", pCallback.m_OwnerSteamID));
		if (CheckVAC(pCallback.m_eAuthSessionResponse))
		{
			Debug.LogError(pCallback.m_eAuthSessionResponse);
			Application.Quit();
		}
		else if (pCallback.m_eAuthSessionResponse != EAuthSessionResponse.k_EAuthSessionResponseAuthTicketCanceled && on_complete_auth_session_request_ != null)
		{
			on_complete_auth_session_request_(pCallback.m_eAuthSessionResponse == EAuthSessionResponse.k_EAuthSessionResponseOK);
			on_complete_auth_session_request_ = null;
		}
		requesting_ = false;
	}

	private bool CheckVAC(EAuthSessionResponse response)
	{
		if (response == EAuthSessionResponse.k_EAuthSessionResponseVACCheckTimedOut || response == EAuthSessionResponse.k_EAuthSessionResponseVACBanned || response == EAuthSessionResponse.k_EAuthSessionResponsePublisherIssuedBan)
		{
			return true;
		}
		return false;
	}
}
